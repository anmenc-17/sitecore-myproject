using Sitecore.Data;

namespace MyProject
{
    public static class Templates
    {
        public static class SiteRoot
        {
            public static readonly ID Id = ID.Parse("{061CBA15-5474-4B91-8A06-17903B102B82}");
        }

        public static class Page
        {
            public static readonly ID Id = ID.Parse("{89829DF8-93EE-56BE-B57A-8F9EFA6BA0A6}");

            public static class Fields
            {
                public static readonly ID Title = ID.Parse("{09265946-B966-5034-BAD7-94E0D935147A}");
            }
        }

        public static class Article
        {
            public static readonly ID Id = ID.Parse("{A79F8C0D-5AC3-444A-882F-F1ED8D800BDD}");
            public static readonly ID TemplateId = ID.Parse("{9AF317C9-7710-4D6C-9749-BFB5C80FFCBB}");
            public static class Fields
            {
                public static readonly ID Image = ID.Parse("{7BF23061-D8B9-4DA9-B5A8-4DDC5BBE6DE5}");
                public static readonly ID Date = ID.Parse("{105F938A-50F1-4FF3-91D7-80045C6BBAF3}");
                public static readonly ID Title = ID.Parse("{7B45C0E9-FF2E-466B-B025-CCB5606BAFD6}");
                public static readonly ID ShowInCarousel = ID.Parse("{01217E01-A2A8-4269-8CC6-6C96BFE02F00}");
            }
        }

        public static class Product
        {
            public static readonly ID Id = ID.Parse("{2123A2D7-996B-4170-A7E6-6C3B4B2BD863}");
            public static readonly ID TemplateId = ID.Parse("{34844FA9-7415-4E68-BC06-FE858748F555}");
            public static class Fields
            {
                public static readonly ID Title = ID.Parse("{07D116E1-8DE7-44E9-B295-E2FD36F47009}"); 
                public static readonly ID Image = ID.Parse("{61436554-630C-48A9-B4EA-876B9271EA4A}");
                public static readonly ID Description = ID.Parse("{44B00E56-99E4-473C-80CF-8B46FF1CBA16}");
                public static readonly ID ShowInCarousel = ID.Parse("{107A1950-445C-4B85-80D9-03B7D70A6921}");
            }
        }
    }
}