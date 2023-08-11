SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [dbo].[PermissionTypes] ([Id])
GO

SET IDENTITY_INSERT [dbo].[PermissionTypes] ON 

INSERT [dbo].[PermissionTypes] ([Id], [Description]) VALUES (1, N'Admin')
INSERT [dbo].[PermissionTypes] ([Id], [Description]) VALUES (2, N'Empleado')
SET IDENTITY_INSERT [dbo].[PermissionTypes] OFF
GO