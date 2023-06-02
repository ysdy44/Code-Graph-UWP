using Code_Graph.Project.Datas;

namespace Code_Graph.Project
{
    static partial class ProjectsExtensions
    {
        public static GroupData[] Samples(string 项目) => new GroupData[]
        {
             new GroupData
             {
                 X = -6, 
                 Y = 3,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 0,
                         Name = "A.csproj",
                         DisplayName = $"{项目} A",
                         Children = new int[]
                         {
                             4,
                             5,
                             6,
                             7,
                             8,
                             9,
                         }
                     },
                     new CsprojData
                     {
                          Index = 2,
                          Name = "B.csproj",
                          DisplayName = $"{项目} B",
                          Children = new int[]
                          {
                             4,
                             5,
                             6,
                             7,
                             8,
                             9,
                          }
                     },
                     new CsprojData
                     {
                         Index = 3,
                         Name = "C.csproj",
                         DisplayName = $"{项目} C",
                         Children = new int[]
                         {
                             4,
                             5,
                             6,
                             7,
                             8,
                             9,
                         }
                     },
                     new CsprojData
                     {
                         Index = 10,
                         Name = "D.csproj",
                         DisplayName = $"{项目} D",
                         Children = new int[]
                         {
                             4,
                             5,
                             6,
                             7,
                             8,
                             9,
                         }
                     }
                 }
             },

             new GroupData
             {
                 X = -25,
                 Y = 2,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 1,
                         Name = "E.csproj",
                         DisplayName = $"{项目} E",
                         Children = new int[]
                         {
                             5,
                         }
                     }
                 }
             },

             new GroupData
             {
                 X = 14,
                 Y = -14,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 4,
                         Name = "F.csproj",
                         DisplayName = $"{项目} F"
                     },
                     new CsprojData
                     {
                         Index = 6,
                         Name = "G.csproj",
                         DisplayName = $"{项目} G"
                     },
                     new CsprojData
                     {
                         Index = 9,
                         Name = "H.csproj",
                         DisplayName = $"{项目} H"
                     }
                 }
             },

             new GroupData
             {
                 X = -6,
                 Y = -16,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 5,
                         Name = "I.csproj",
                         DisplayName = $"{项目} I"
                     }
                 }
             },

             new GroupData
             {
                 X = 15,
                 Y = 19,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 7,
                         Name = "J.csproj",
                         DisplayName = $"{项目} J",
                         Children = new int[]
                         {
                             8
                         }
                     }
                 }
             },

             new GroupData
             {
                 X = 23,
                 Y = 3,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 8,
                         Name = "K.csproj",
                         DisplayName = $"{项目} K"
                     }
                 }
             },

             new GroupData
             {
                 X = -23,
                 Y = 17,
                 Source = new CsprojData[]
                 {
                     new CsprojData
                     {
                         Index = 11,
                         Name = "L.csproj",
                         DisplayName = $"{项目} L"
                     }
                 }
             }
        };
    }
}