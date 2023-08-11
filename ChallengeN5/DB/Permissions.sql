SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeId] [int] NULL,
	[EmployeeName] [varchar](255) NOT NULL,
	[EmployeeLastName] [varchar](255) NOT NULL,
	[PermissionType] [int] NOT NULL,
	[PermissionDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([Id], [TypeId], [EmployeeName], [EmployeeLastName], [PermissionType], [PermissionDate]) VALUES (1, 1, N'Santiago', N'Alfonso', 1, CAST(N'2023-04-25' AS Date))
INSERT [dbo].[Permissions] ([Id], [TypeId], [EmployeeName], [EmployeeLastName], [PermissionType], [PermissionDate]) VALUES (2, 2, N'Pablo', N'Herrera', 2, CAST(N'2023-08-11' AS Date))
INSERT [dbo].[Permissions] ([Id], [TypeId], [EmployeeName], [EmployeeLastName], [PermissionType], [PermissionDate]) VALUES (3, 1, N'Paola', N'Lozano', 2, CAST(N'2023-08-11' AS Date))
INSERT [dbo].[Permissions] ([Id], [TypeId], [EmployeeName], [EmployeeLastName], [PermissionType], [PermissionDate]) VALUES (5, 2, N'Lucas', N'Cardozo', 2, CAST(N'2023-08-10' AS Date))
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO