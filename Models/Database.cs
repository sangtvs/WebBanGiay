using System.Data.SQLite;
namespace testt.Models
{
    public class Database
    {
        private SQLiteConnection connection;
        private string databaseName = "Database.db";

        public Database()
        {
            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            connection = new SQLiteConnection($"Data Source={databaseName}; Version=3;");
            connection.Open();
        }
        public User GetUsers(User model)
        {
            User users = null;
                string query = "SELECT * FROM Users WHERE UserName=@UserName AND PassWord=@PassWord";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Name = reader.GetString(3);
                            users.Email = reader.GetString(4);
                            users.Phone = reader.GetString(5);
                            users.Role = reader.GetString(6);
                            users.Address = reader.GetString(7);
                            users.Sex = reader.GetString(8);
                        string role = reader.GetString(6); // Ở đây, giả sử cột role có chỉ số là 5
                            if (role == "admin")
                            {
                                // Người dùng có vai trò là admin
                                users.Role = "admin";
                            }
                            else
                            {
                                // Người dùng có vai trò khác
                                users.Role = "user";
                            }
                        }
                    }
                }

 

            return users;
        }
        public User CheckUsers(User model)
        {
            User users = null;
                string query = "SELECT * FROM Users WHERE UserName=@UserName";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            users = new User(); // Gán giá trị cho users nếu có bản ghi phù hợp
                            users.UserName = reader.GetString(1);
                        }
                    }
                }

            return users;
        }
        public void AddUser(User model)
        {
                string query = "INSERT INTO Users (Name,UserName, PassWord,Phone, Email, Role,Address,Sex) VALUES (@Name,@UserName, @PassWord,@Phone, @Email, @Role,@Address,@Sex)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", model.Name);
                    command.Parameters.AddWithValue("@UserName", model.UserName);
                    command.Parameters.AddWithValue("@PassWord", model.PassWord);
                    command.Parameters.AddWithValue("@Phone", model.Phone);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Role", "user");
                    command.Parameters.AddWithValue("@Address",model.Address);
                    command.Parameters.AddWithValue("@Sex", " ");
                    command.ExecuteNonQuery();
                }
        }
        public void Themkh(User model)
        {
            string query = "INSERT INTO Users (Name,UserName,Password,Phone, Email,Address,Sex, Role) VALUES (@Name,@UserName,@Password,@Phone,  @Email,@Address,@Sex, @Role)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", model.Name);
                command.Parameters.AddWithValue("@UserName", " ");
                command.Parameters.AddWithValue("@Password", " ");
                command.Parameters.AddWithValue("@Phone", model.Phone);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Sex", model.Sex);
                command.Parameters.AddWithValue("@Role", "user");
                command.ExecuteNonQuery();
            }
        }
        public List<User> LoadUser()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM Users";

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        ID = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        PassWord = reader.GetString(2),
                        Name = reader.GetString(3),
                        Email = reader.GetString(4),
                        Phone = reader.GetString(5),
                        Role = reader.GetString(6),
                        Address = reader.GetString(7),
                        Sex = reader.GetString(8),
                    });
                }
            }
            return users;
        }
        public User GetUsers(int id)
        {
            User users = new User();
                string query = "SELECT * FROM Users WHERE ID=@ID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.ID = reader.GetInt32(0);
                            users.UserName = reader.GetString(1);
                            users.PassWord = reader.GetString(2);
                            users.Name = reader.GetString(3);
                            users.Email = reader.GetString(4);
                            users.Phone = reader.GetString(5);
                            users.Role = reader.GetString(6);
                            users.Address = reader.GetString(7);
                            users.Sex = reader.GetString(8);

                        }
                    }
                }
            return users;
        }
        public void saveUser(User model)
        {
                string query = "UPDATE Users SET UserName=@user,PassWord=@pass,Email=@email, Phone=@mobile,Name=@name,Address=@address,Sex=@sex WHERE ID=@id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", model.ID);
                command.Parameters.AddWithValue("@user", model.UserName);
                command.Parameters.AddWithValue("@pass", model.PassWord);
                command.Parameters.AddWithValue("@mobile", model.Phone);
                command.Parameters.AddWithValue("@email", model.Email);
                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@address", model.Address);
                command.Parameters.AddWithValue("@sex", model.Sex);
                command.ExecuteNonQuery();
            }

        }
        public void DeleteUser(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE ID = @ID";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
        public void ThemGiay(Products model)
        {
            string query = "INSERT INTO Products (ProductName,Description,Price,QuantityInStock,Brand,ProductType,Image) VALUES" +
                " (@ProductName,@Description,@Price,@QuantityInStock, @Brand, @ProductType,@Image)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", model.ProductName);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@Price", model.Price);
                command.Parameters.AddWithValue("@QuantityInStock", model.QuantityInStock);
                command.Parameters.AddWithValue("@Brand", model.Brand);
                command.Parameters.AddWithValue("@ProductType", model.ProductType);
                command.Parameters.AddWithValue("@Image", model.Image);
                command.ExecuteNonQuery();
            }
        }
        public List<Products> GetGiay()
        {
            string query = "SELECT * FROM Products";
            List<Products> giay = new List<Products>();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] imageData = reader["Image"] as byte[];
                    giay.Add(new Products
                    {
                        Id = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetInt32(3),
                        QuantityInStock = reader.GetInt32(4),
                        Brand = reader.GetString(5),
                        ProductType = reader.GetString(6),
                        Image = imageData
                    });
                }
            return giay;
        }
        public Products GetGiay(int id)
        {
            string query = "SELECT * FROM Products";
            Products giay = new Products();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                byte[] imageData = reader["Image"] as byte[];

                giay.Id = reader.GetInt32(0);
                giay.ProductName = reader.GetString(1);
                giay.Description = reader.GetString(2);
                giay.Price = reader.GetInt32(3);
                giay.QuantityInStock = reader.GetInt32(4);
                giay.Brand = reader.GetString(5);
                giay.ProductType = reader.GetString(6);
                giay.Image = imageData;
                
            }
            return giay;
        }
        public byte[] GetAnh(int id)
        {
            string query = "SELECT * FROM Products WHERE ID=@ID";
            byte[] imageData = null;
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", id);
            SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    imageData = reader["Image"] as byte[];
                }
            return imageData;
        }
        public Products DetailGiay(int id)
        {
            string query = "SELECT * FROM Products WHERE ID=@ID";
            Products giay = new Products();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] imageData = reader["Image"] as byte[];
                        giay.Id = reader.GetInt32(0);
                        giay.ProductName = reader.GetString(1);
                        giay.Description = reader.GetString(2);
                        giay.Price = reader.GetInt32(3);
                        giay.QuantityInStock = reader.GetInt32(4);
                        giay.Brand = reader.GetString(5);
                        giay.ProductType = reader.GetString(6);
                        giay.Image = imageData;
                    }
                }
            }

            return giay;
        }
        public void DeleteGiay(int id)
        {
            string deleteQuery = "DELETE FROM Products WHERE ID = @ID";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.ExecuteNonQuery();
            }
        }
        public Orders GetOders(int id)
        {
            Orders orders = new Orders();
            string query = "SELECT * FROM Orders WHERE UserID=@ID and OrderStatus=@status";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@status", "chuahoanthanh");
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.ID = reader.GetInt32(0);
                        orders.UserID = reader.GetInt32(1);
                        orders.OrderDate = reader.GetString(2);
                        orders.TotalAmount = reader.GetInt32(3);
                        orders.OrderStatus= reader.GetString(4);
                    }
                }
            }
            return orders;
        }
        public void AddOrders(int id)
        {
            string query = "INSERT INTO Orders (UserID,OrderDate,TotalAmount,OrderStatus) VALUES (@userid,@orderdate, @totalamount,@status)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userid", id);
                command.Parameters.AddWithValue("@orderdate",DateTime.Now);
                command.Parameters.AddWithValue("@totalamount", 0);
                command.Parameters.AddWithValue("@status","chuahoanthanh");
                command.ExecuteNonQuery();
            }
        }
        public void AddOrderDetails(OrderDetails model)
        {
            string query = "INSERT INTO OrderDetails (OrderID,ProductID, Quantity,Price,Size) VALUES (@orderid,@productid, @quantity,@price,@size)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderid", model.OrderId);
                command.Parameters.AddWithValue("@productid",model.ProductID);
                command.Parameters.AddWithValue("@quantity", model.Quantity);
                command.Parameters.AddWithValue("@price", model.Price);
                command.Parameters.AddWithValue("@size", model.Size);
                command.ExecuteNonQuery();
            }
        }
        public void saveOders(Orders model)
        {
            string query = "UPDATE Orders SET TotalAmount=@totalamount WHERE ID=@id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", model.ID);
                command.Parameters.AddWithValue("@totalamount", model.TotalAmount);
                command.ExecuteNonQuery();
            }

        }
        public List<OrderDetails> GetOrderDetail(int orderId)
        {
            string query = "SELECT od.Id, od.OrderId, od.ProductId, od.Quantity, od.Price,od.Size, p.ProductName " +
                           "FROM OrderDetails od " +
                           "INNER JOIN Products p ON od.ProductId = p.Id " +
                           "WHERE od.OrderId = @orderId";

            List<OrderDetails> orderDetails = new List<OrderDetails>();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderId", orderId);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderDetails orderDetail = new OrderDetails
                        {
                            Id = reader.GetInt32(0),
                            OrderId = reader.GetInt32(1),
                            ProductID = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            Price = reader.GetInt32(4),
                            Size= reader.GetInt32(5),
                            ProductName = reader.GetString(6)
                        };
                        orderDetails.Add(orderDetail);
                    }
                }
            }
            return orderDetails;
        }
        public void DeleteProduct(int id,int idoder)
        {
            string deleteQuery = "DELETE FROM OrderDetails WHERE ID = @ID AND OrderID=@idorder";
            using (var command = new SQLiteCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@idorder", idoder);
                command.ExecuteNonQuery();
            }
        }
        public void UpdateOrderStatus(int orderId)
        {
            string query = "UPDATE Orders SET OrderStatus = 'Hoàn thành' WHERE ID = @orderId";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@orderId", orderId);
                command.ExecuteNonQuery();
            }        }
        public void ReduceProductQuantity(int productId, int quantity)
        {
            string query = "UPDATE Products SET QuantityInStock = QuantityInStock - @quantity WHERE ID = @productId";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@productId", productId);
                command.ExecuteNonQuery();
            }
        }
        public void saveGiay(Products model)
        {
            string query = "UPDATE Products SET ProductName=@name,Description=@mieuta,Price=@gia,QuantityInStock=@tonkho,Brand=@hang,ProductType=@type WHERE ID=@id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", model.Id);
                command.Parameters.AddWithValue("@name", model.ProductName);
                command.Parameters.AddWithValue("@mieuta", model.Description);
                command.Parameters.AddWithValue("@gia", model.Price);
                command.Parameters.AddWithValue("@tonkho", model.QuantityInStock);
                command.Parameters.AddWithValue("@hang", model.Brand);
                command.Parameters.AddWithValue("@type", model.ProductType);
                command.ExecuteNonQuery();
            }

        }
    }

}

