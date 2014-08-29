using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class DataHelper : IDataHelper
    {
        private string PinConnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        private static AmazonDynamoDBClient client;

        public bool InsertUser(Dictionary<string, object> us)
        {
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);

            try
            {
                PutItemRequest putReq = new PutItemRequest
                {
                    TableName = "User",
                    Item = new Dictionary<string, AttributeValue>() { 
                        { "UserID", new AttributeValue { S = us["userID"].ToString() } } ,
                        { "UserName", new AttributeValue { S = us["userName"].ToString() } } ,
                        { "HeadshotURL", new AttributeValue { S = us["headshotURL"].ToString() } } ,
                        { "IsPayUser", new AttributeValue { S = us["IsPayUser"].ToString() } } ,
                        { "RegistDate", new AttributeValue { N = us["registDate"].ToString() } } ,
                        { "LastLoginDate", new AttributeValue { N = us["lastLoginDate"].ToString() } } ,
                        { "LastSyncDate", new AttributeValue { N = us["lastSyncDate"].ToString() } } 
                    }
                };

                PutItemResponse response = client.PutItem(putReq);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            //using (PinContext context = new PinContext(PinConnString))
            //{

            //    context.Entry(us).State = EntityState.Added;

            //    context.SaveChanges();
            //}
            return true;
        }

        public bool CheckUserIsExist(string userID)
        {
            var config = new AmazonDynamoDBConfig();
            GetItemResponse response;
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);
            bool retval = false;
            try
            {
                GetItemRequest request = new GetItemRequest
                {
                    TableName = "User",
                    Key = new Dictionary<string, AttributeValue>() { { "UserID", new AttributeValue { S = userID } } },
                    ReturnConsumedCapacity = "TOTAL"
                };
                response = client.GetItem(request);
                retval = response.Item.Count > 0;
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return retval;
        }

        public Dictionary<string, object> GetUser(string userID)
        {
            Dictionary<string, object> retval = new Dictionary<string, object>();
            GetItemResponse response;
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);
            try
            {
                GetItemRequest request = new GetItemRequest
                {
                    TableName = "User",
                    Key = new Dictionary<string, AttributeValue>() { { "UserID", new AttributeValue { S = userID } } },
                    ReturnConsumedCapacity = "TOTAL"
                };
                response = client.GetItem(request);
                retval.Add("UserID", response.Item["UserID"].S);
                retval.Add("HeadshotURL", response.Item["HeadshotURL"].S);
                retval.Add("IsPayUser", Convert.ToBoolean(response.Item["IsPayUser"].S));
                retval.Add("UserName", response.Item["UserName"].S);
                retval.Add("RegistDate", response.Item["RegistDate"].N);
                retval.Add("LastLoginDate", response.Item["LastLoginDate"].N);
                retval.Add("LastSyncDate", response.Item["LastSyncDate"].N);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }


            return retval;
        }
        public bool StorePin(Dictionary<string, object> pn)
        {
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);

            try
            {
                PutItemRequest putReq = new PutItemRequest
                {
                    TableName = "Pin",
                    Item = new Dictionary<string, AttributeValue>() { 
                        { "Owner", new AttributeValue { S = pn["OwnerID"].ToString() } } ,
                        { "Title", new AttributeValue { S = pn["Title"].ToString() } } ,
                        { "PinDate", new AttributeValue { N = pn["PinDate"].ToString() } } ,
                        { "Latitude", new AttributeValue { S = pn["Latitude"].ToString() } } ,
                        { "Longitude", new AttributeValue { S = pn["Longitude"].ToString() } } ,
                        { "Images", new AttributeValue { SS = (List<string>)pn["Images"] } } ,
                        { "HeadshotURL", new AttributeValue { S = pn["OwnerHeadshot"].ToString() } } ,
                        { "UserName", new AttributeValue { S = pn["OwnerName"].ToString() } } ,
                        { "LastSyncDate", new AttributeValue { N = pn["LastSyncDate"].ToString() } } 
                    }
                };

                PutItemResponse response = client.PutItem(putReq);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return true;
        }

        public Dictionary<string, object> GetPinWithPinID(string owner, double pinDate)
        {
            Dictionary<string, object> retval = new Dictionary<string, object>();
            QueryResponse response;
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);
            try
            {
                QueryRequest request = new QueryRequest()
                {
                    TableName = "Pin",
                    KeyConditions = new Dictionary<string, Condition>() 
                    {
                        { 
                            "Owner",
                            new Condition() 
                            { 
                                ComparisonOperator = ComparisonOperator.EQ,
                                AttributeValueList = new List<AttributeValue> { new AttributeValue { S = owner } } 
                            }
                        },
                        { 
                            "PinDate",
                            new Condition() 
                            { 
                                ComparisonOperator = ComparisonOperator.EQ,
                                AttributeValueList = new List<AttributeValue> { new AttributeValue { N = pinDate.ToString() } } 
                            }
                        }
                    }
                    
                };
                response = client.Query(request);
                retval.Add("Title", response.Items[0]["Title"].S);
                retval.Add("Owner", response.Items[0]["Owner"].S);
                retval.Add("OwnerName", response.Items[0]["UserName"].S);
                retval.Add("OwnerHeadshot", response.Items[0]["HeadshotURL"].S);
                retval.Add("Latitude", response.Items[0]["Latitude"].S);
                retval.Add("Longitude", response.Items[0]["Longitude"].S);
                retval.Add("PinDate", Convert.ToDouble(response.Items[0]["PinDate"].N));
                retval.Add("Images", response.Items[0]["Images"].SS);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }


            return retval;
        }

        public List<Dictionary<string, object>> GetPinWithUserID(string userID, double since, int takeCnt)
        {
            List<Dictionary<string, object>> retval = new List<Dictionary<string, object>>();
            Dictionary<string, object> tmpObject = null;
            QueryResponse response = null;
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);
            try
            {
                QueryRequest query = new QueryRequest()
                {
                    TableName = "Pin",
                    KeyConditions = new Dictionary<string, Condition>() 
                    {
                        { 
                            "Owner",
                            new Condition() 
                            { 
                                ComparisonOperator = ComparisonOperator.EQ,
                                AttributeValueList = new List<AttributeValue> { new AttributeValue { S = userID } } 
                            }
                        },
                        { 
                            "PinDate",
                            new Condition() 
                            { 
                                ComparisonOperator = ComparisonOperator.LT,
                                AttributeValueList = new List<AttributeValue> { new AttributeValue { N = since.ToString() } } 
                            }
                        }
                    },
                    Limit = takeCnt,
                    ScanIndexForward = false

                };
                
                response = client.Query(query);
                foreach (var item in response.Items)
                {
                    tmpObject = new Dictionary<string, object>();
                    tmpObject.Add("Title", item["Title"].S);
                    tmpObject.Add("Owner", item["Owner"].S);
                    tmpObject.Add("OwnerName", item["UserName"].S);
                    tmpObject.Add("OwnerHeadshot", item["HeadshotURL"].S);
                    tmpObject.Add("Latitude", item["Latitude"].S);
                    tmpObject.Add("Longitude", item["Longitude"].S);
                    tmpObject.Add("PinDate", Convert.ToDouble(item["PinDate"].N));
                    tmpObject.Add("Images", item["Images"].SS);
                    retval.Add(tmpObject);
                }
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }


            return retval;
        }
        public List<Dictionary<string, object>> GetPinWithUserIDs(List<string> userIDs, double since, int takeCnt)
        {
            List<Dictionary<string, object>> retval = new List<Dictionary<string, object>>();
            Dictionary<string, object> tmpObject = null;
            
            var config = new AmazonDynamoDBConfig();
            config.ServiceURL = System.Configuration.ConfigurationManager.AppSettings["ServiceURL"];
            client = new AmazonDynamoDBClient(config);
            List<AttributeValue> users = new List<AttributeValue>();
            foreach (string item in userIDs)
            {
                users.Add(new AttributeValue() { S = item });
            }
            try
            {
                ScanRequest sreq = new ScanRequest()
                {
                    TableName = "Pin",
                    ScanFilter = new Dictionary<string, Condition>()
                    {
                        {   "Owner", 
                            new Condition
                            {
                                ComparisonOperator = ComparisonOperator.IN,
                                AttributeValueList = users
                            }
                        },
                        { 
                            "PinDate",
                            new Condition() 
                            { 
                                ComparisonOperator = ComparisonOperator.LT,
                                AttributeValueList = new List<AttributeValue> { new AttributeValue { N = since.ToString() } } 
                            }
                        }
                    }
                };

                var response = client.Scan(sreq);
                foreach (var item in response.Items)
                {
                    tmpObject = new Dictionary<string, object>();
                    tmpObject.Add("Title", item["Title"].S);
                    tmpObject.Add("Owner", item["Owner"].S);
                    tmpObject.Add("OwnerName", item["UserName"].S);
                    tmpObject.Add("OwnerHeadshot", item["HeadshotURL"].S);
                    tmpObject.Add("Latitude", item["Latitude"].S);
                    tmpObject.Add("Longitude", item["Longitude"].S);
                    tmpObject.Add("PinDate", Convert.ToDouble(item["PinDate"].N));
                    tmpObject.Add("Images", item["Images"].SS);
                    retval.Add(tmpObject);
                }
                retval = retval.OrderByDescending(a => a["PinDate"]).ToList<Dictionary<string, object>>();
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            
            return retval;

        }
    }
}
