CREATE TABLE [dbo].[YJ_XZ_DAB](
	[DABH] [nvarchar](50) NOT NULL,
	[JDJG] [nvarchar](max) NULL,
	[YFDQ] [nvarchar](max) NULL,
	[QZXM] [nvarchar](40) NULL,
	[QZCSNY] [datetime] NULL,
	[QZZJHM] [nvarchar](50) NULL,
	[ZFXM] [nvarchar](40) NULL,
	[ZFCSNY] [datetime] NULL,
	[ZFZJHM] [nvarchar](50) NULL,
	[JDRQ] [datetime] NULL,
	[PGJYWCRQ] [datetime] NULL,
	[PGJYZT] [nvarchar](max) NULL,
 CONSTRAINT [PK_YJ_XZ_DAB] PRIMARY KEY CLUSTERED 
(
	[DABH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]