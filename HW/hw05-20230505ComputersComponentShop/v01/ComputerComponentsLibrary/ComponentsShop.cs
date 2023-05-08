namespace ComputerComponentsLibrary
{
    public class ComponentsShop
    {
        public static List<CategoryComponent> GetCategoriesList()
        {
            /*
            return new List<CategoryComponent>()
            {
                new CategoryComponent(1, "Материнська плата"),
                new CategoryComponent(2, "Процессор"),
                new CategoryComponent(3, "SSD накопичувач"),
                new CategoryComponent(4, "Відеокарта"),
                new CategoryComponent(5, "HDD накопичувач"),
                new CategoryComponent(6, "Оперативна пам'ять"),
                new CategoryComponent(7, "Корпус"),
                new CategoryComponent(8, "Блок живлення"),
                new CategoryComponent(9, "Монітор"),
            };
            */
            List<CategoryComponent> list = new List<CategoryComponent>();
            list.Add(new CategoryComponent(1, "Материнська плата"));
            list.Add(new CategoryComponent(2, "Процессор"));
            list.Add(new CategoryComponent(3, "SSD накопичувач"));
            list.Add(new CategoryComponent(4, "Відеокарта"));
            list.Add(new CategoryComponent(5, "HDD накопичувач"));
            list.Add(new CategoryComponent(6, "Оперативна пам'ять"));
            list.Add(new CategoryComponent(7, "Корпус"));
            list.Add(new CategoryComponent(8, "Блок живлення"));
            list.Add(new CategoryComponent(9, "Монітор"));
            return list;
        }

        public static List<Component> GetComponentsList()
        {
            List<CategoryComponent> categoriesList = GetCategoriesList();

            List<Component> list = new List<Component>();
            list.Add(new Component(1, "ASUS PRIME H510M-K", 2548, categoriesList[0]));
            list.Add(new Component(2, "GIGABYTE B550 GAMING X V2", 4599, categoriesList[0]));
            list.Add(new Component(3, "ASRock X570 PHANTOM GAMING 4", 5599, categoriesList[0]));
            list.Add(new Component(4, "INTEL Core™ i3 10105", 4999, categoriesList[1]));
            list.Add(new Component(5, "AMD Ryzen 5 5600", 6069, categoriesList[1]));
            list.Add(new Component(6, "INTEL Core™ i5 11400F", 5099, categoriesList[1]));
            list.Add(new Component(7, "SSD M.2 2280 500GB Samsung", 1665, categoriesList[2]));
            list.Add(new Component(8, "SSD M.2 2280 1TB Kingston", 2329, categoriesList[2]));
            list.Add(new Component(9, "SSD M.2 2280 256GB ADATA", 959, categoriesList[2]));
            list.Add(new Component(10, "ASUS GeForce RTX3060 12Gb DUAL OC V2 LHR", 16399, categoriesList[3]));
            list.Add(new Component(11, "GIGABYTE Radeon RX 6500 XT 4Gb GAMING OC", 8328, categoriesList[3]));
            list.Add(new Component(12, "ASUS GeForce RTX3050 8Gb DUAL OC", 12099, categoriesList[3]));
            list.Add(new Component(13, "HDD 3.5\" 1TB WD", 1470, categoriesList[4]));
            list.Add(new Component(14, "HDD 3.5\" 1TB Toshiba", 1335, categoriesList[4]));
            list.Add(new Component(15, "HDD 3.5\" 4TB Seagate", 3139, categoriesList[4]));
            list.Add(new Component(16, "DDR4 16GB (2x8GB) 3200 MHz Fury Beast Black Kingston", 1819, categoriesList[5]));
            list.Add(new Component(17, "DDR4 32GB (2x16GB) 3200 MHz Fury Beast Black", 3399, categoriesList[5]));
            list.Add(new Component(18, "DDR4 32GB (2x16GB) 3200 MHz Fury Beast Black Kingston Fury", 3099, categoriesList[5]));
            list.Add(new Component(19, "Vinga Sky-500W", 1669, categoriesList[6]));
            list.Add(new Component(20, "AeroCool Skribble-G-BK-v1", 2899, categoriesList[6]));
            list.Add(new Component(21, "MSI MPG GUNGNIR 100", 4899, categoriesList[6]));
            list.Add(new Component(22, "Chieftec 500W", 1409, categoriesList[7]));
            list.Add(new Component(23, "Seasonic 750W FOCUS GM-750", 6194, categoriesList[7]));
            list.Add(new Component(24, "Vinga 750W (VPS-750GV2)", 3259, categoriesList[7]));
            list.Add(new Component(25, "Lenovo L24i-30", 4599, categoriesList[8]));
            list.Add(new Component(26, "Samsung LF24T350FHIXCI", 4999, categoriesList[8]));
            list.Add(new Component(27, "LG 29WP500-B", 7999, categoriesList[8]));
            return list;
        }
    }
}