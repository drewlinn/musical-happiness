USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 6/21/2017 1:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[genre] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[shows]    Script Date: 6/21/2017 1:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shows](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_id] [int] NULL,
	[venue_id] [int] NULL,
	[date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 6/21/2017 1:46:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[address] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (2, N'Tom Waits', N'Experimental')
INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (3, N'Radiohead', N'Experimental Rock')
INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (4, N'Nick Cave and the Bad Seeds', N'Art Rock')
INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (5, N'Nick Drake', N'Folk/Blues')
INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (6, N'Leonard Cohen', N'Folk/Soft Rock')
INSERT [dbo].[bands] ([id], [name], [genre]) VALUES (7, N'Explosions in the Sky', N'Post-Rock')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[shows] ON 

INSERT [dbo].[shows] ([id], [band_id], [venue_id], [date]) VALUES (2, 2, 1, CAST(N'1990-05-06T21:00:00.000' AS DateTime))
INSERT [dbo].[shows] ([id], [band_id], [venue_id], [date]) VALUES (3, 5, 5, CAST(N'1966-06-06T18:00:00.000' AS DateTime))
INSERT [dbo].[shows] ([id], [band_id], [venue_id], [date]) VALUES (4, 2, 1, CAST(N'2017-01-01T01:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[shows] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name], [address]) VALUES (1, N'Dave''s Bar', N'1234 Baker St.')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (2, N'The Colosseum', N'890 Delta Road')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (3, N'In the Venue', N'1214 Grand Central Ave.')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (4, N'Burt''s Tiki Lounge', N'456 State St.')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (5, N'The Doug Fir Lounge', N'2014 W Burnside')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (6, N'Mississipi Studios', N'1345 N Mississippi St.')
INSERT [dbo].[venues] ([id], [name], [address]) VALUES (7, N'Toby''s', N'1234 Street')
SET IDENTITY_INSERT [dbo].[venues] OFF
