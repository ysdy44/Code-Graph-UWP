using Code_Graph.Project;
using Code_Graph.Project.Datas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Shapes;

namespace Code_Graph
{
    partial class MainPage
    {
        public async void Click(OptionType type)
        {
            switch (type)
            {
                case OptionType.Update:
                    {
                        for (int i = 0; i < this.Links.Length; i++)
                        {
                            KeyValuePair<int, int> link = this.Links[i];
                            Group key = this.Groups[link.Key];
                            Group value = this.Groups[link.Value];

                            Line line = this.LineChildren[i] as Line;
                            line.X1 = key.X * 10;
                            line.Y1 = key.Y * 10;
                            line.X2 = value.X * 10;
                            line.Y2 = value.Y * 10;

                            Ellipse ellipse = this.EllipseChildren[i] as Ellipse;
                            Thumb thumb = this[link.Value];
                            double w = thumb.ActualWidth;
                            double h = thumb.ActualHeight;

                            Point arrow = new IntersectsRectAndLine(w, h, value.X * 10, value.Y * 10, key.X * 10, key.Y * 10).Arrow;
                            Canvas.SetLeft(ellipse, arrow.X - 6);
                            Canvas.SetTop(ellipse, arrow.Y - 6);
                        }
                    }
                    break;

                case OptionType.Clear:
                    {
                        foreach (Thumb thumb in this.ThumbChildren)
                        {
                            thumb.SizeChanged -= this.DragSizeChanged;
                            thumb.DragStarted -= this.DragStarted;
                            thumb.DragDelta -= this.DragDelta;
                            thumb.DragCompleted -= this.DragCompleted;
                        }
                        this.LineChildren.Clear();
                        this.EllipseChildren.Clear();
                        this.ThumbChildren.Clear();
                    }
                    break;
                case OptionType.Add:
                    {
                        for (int i = 0; i < this.Links.Length; i++)
                        {
                            this.LineChildren.Add(new Line());
                            this.EllipseChildren.Add(new Ellipse());
                        }

                        foreach (Group item in this.Groups)
                        {
                            if (item.Level == default) continue;

                            Thumb thumb = new Thumb
                            {
                                Tag = this.Files.DisplayName(item.Source),
                                Style = this[item.Level]
                            };
                            thumb.SizeChanged += this.DragSizeChanged;
                            thumb.DragStarted += this.DragStarted;
                            thumb.DragDelta += this.DragDelta;
                            thumb.DragCompleted += this.DragCompleted;
                            this.ThumbChildren.Add(thumb);
                        }
                    }
                    break;

                case OptionType.New:
                    {
                        // FolderPicker
                        FolderPicker folderPicker = new FolderPicker
                        {
                            SuggestedStartLocation = default,
                            FileTypeFilter =
                            {
                                "*"
                            }
                        };

                        // PickSingleFolderAsync
                        StorageFolder folder = await folderPicker.PickSingleFolderAsync();
                        if (folder is null) return;

                        IList<CsprojLine> lines = new List<CsprojLine>();
                        foreach (StorageFolder item in await folder.GetFoldersAsync())
                        {
                            foreach (StorageFile file in await item.GetFilesAsync())
                            {
                                if (file.FileType == ProjectsExtensions.FileType)
                                {
                                    IList<string> lines2 = await FileIO.ReadLinesAsync(file);
                                    lines.Add(new CsprojLine
                                    {
                                        Name = file.Name,
                                        DisplayName = file.DisplayName,
                                        Lines = lines2.Where(ProjectsExtensions.Contains).ToArray()
                                    });
                                }
                            }
                        }

                        this.Click(OptionType.Clear);
                        if (lines.Count == 0) break;

                        this.Files = lines.ToFiles();
                        this.Groups = this.Files.ToGroups(0, 0).ToArray();
                        this.Links = this.Groups.ToLines(this.Files).ToArray();
                        this.Click(OptionType.Add);

                        this.Click(OptionType.Update);
                    }
                    break;
                case OptionType.Save:
                    {
                        string suggestedFileName = "Untitled";

                        // FileSavePicker
                        FileSavePicker savePicker = new FileSavePicker
                        {
                            SuggestedStartLocation = default,
                            SuggestedFileName = suggestedFileName,
                            FileTypeChoices =
                            {
                                {"DB", new[] { ProjectsExtensions.FileChoices } }
                            }
                        };

                        // PickSaveFileAsync
                        StorageFile file = await savePicker.PickSaveFileAsync();
                        if (file is null) break;

                        if (true)
                        {
                            await FileIO.WriteTextAsync(file, new XDocument
                            (
                                new XElement("Root", this.Groups.Select(this.Save))
                            ).ToString());
                        }
                        else
                        {
                            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                new XDocument
                                (
                                    new XElement("Root", this.Groups.Select(this.Save))
                                ).Save(stream.AsStream());
                            }
                        }
                    }
                    break;
                case OptionType.Load:
                    {
                        // FileOpenPicker
                        FileOpenPicker openPicker = new FileOpenPicker
                        {
                            SuggestedStartLocation = default,
                            FileTypeFilter =
                            {
                                { ProjectsExtensions.FileChoices }
                            }
                        };

                        // PickSingleFileAsync
                        StorageFile file = await openPicker.PickSingleFileAsync();
                        if (file is null) break;

                        this.Click(OptionType.Clear);
                        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            XDocument document = XDocument.Load(stream.AsStream());
                            IEnumerable<GroupData> datas = this.Load(document);
                            if (datas.Count() == 0) break;

                            this.Files = datas.ToFiles();
                            this.Groups = this.Files.ToGroups(datas).ToArray();
                            this.Links = this.Groups.ToLines(this.Files).ToArray();
                            this.Click(OptionType.Add);

                            this.Click(OptionType.Update);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}