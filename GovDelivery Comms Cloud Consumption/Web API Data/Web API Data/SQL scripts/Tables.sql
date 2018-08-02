/****** Object:  Table [dbo].[tblBulletin]    Script Date: 4/1/2018 10:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBulletin](
	[key] [int] IDENTITY(1,1) NOT NULL,
	[bulletin_activity_details_Id] [nvarchar](50) NULL,
	[userAcountId] [nvarchar](50) NULL,
	[created_at] [datetime] NULL,
	[subject] [nvarchar](max) NULL,
	[to_text] [nvarchar](max) NULL,
	[delivery_status_name] [nvarchar](50) NULL,
	[addresses_count] [int] NULL,
	[success_count] [nvarchar](50) NULL,
	[failed_count] [nvarchar](50) NULL,
	[percent_success] [nvarchar](50) NULL,
	[immediate_email_recipients] [nvarchar](50) NULL,
	[emails_delivered] [nvarchar](50) NULL,
	[emails_failed] [nvarchar](50) NULL,
	[percent_emails_delivered] [nvarchar](50) NULL,
	[opens_count] [nvarchar](50) NULL,
	[percent_opened] [nvarchar](50) NULL,
	[links_count] [nvarchar](50) NULL,
	[click_rate] [nvarchar](50) NULL,
	[clicks_count] [nvarchar](50) NULL,
	[nonunique_clicks_count] [nvarchar](50) NULL,
	[wireless_recipients] [nvarchar](50) NULL,
	[wireless_delivered] [nvarchar](50) NULL,
	[wireless_failed_count] [nvarchar](50) NULL,
	[bulletin_visibility?] [nvarchar](50) NULL,
	[publish_to_facebook] [nvarchar](50) NULL,
	[publish_to_twitter] [nvarchar](50) NULL,
	[publish_to_rss?] [nvarchar](50) NULL,
	[wireless_unique_clicks] [nvarchar](50) NULL,
	[wireless_nonunique_clicks] [nvarchar](50) NULL,
	[facebook_nonunique_clicks] [nvarchar](50) NULL,
	[twitter_nonunique_clicks] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblActivity] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBulletinLink]    Script Date: 4/1/2018 10:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBulletinLink](
	[bulletin_activity_details_Id] [nvarchar](50) NULL,
	[_links_Id] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLinkDetails]    Script Date: 4/1/2018 10:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLinkDetails](
	[key] [int] IDENTITY(1,1) NOT NULL,
	[link_url] [nvarchar](max) NULL,
	[subject] [nvarchar](max) NULL,
	[sent_at] [datetime] NULL,
	[sender_email] [nvarchar](50) NULL,
	[unique_click_count] [nvarchar](50) NULL,
	[total_click_count] [nvarchar](50) NULL,
	[bulletins_links_details_Id] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblLinkDetails] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblLinkHref]    Script Date: 4/1/2018 10:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLinkHref](
	[_links_Id] [nvarchar](50) NULL,
	[href] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblUserAccount]    Script Date: 4/1/2018 10:08:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserAccount](
	[key] [int] NOT NULL,
	[userFirstName] [nvarchar](50) NULL,
	[userLastName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO