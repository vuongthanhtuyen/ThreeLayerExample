using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
// <auto-generated />
namespace SweetCMS.DataAccess{
    /// <summary>
    /// Strongly-typed collection for the StatisticsUserAndTotalMoney class.
    /// </summary>
    [Serializable]
    public partial class StatisticsUserAndTotalMoneyCollection : ReadOnlyList<StatisticsUserAndTotalMoney, StatisticsUserAndTotalMoneyCollection>
    {        
        public StatisticsUserAndTotalMoneyCollection() {}
    }
    /// <summary>
    /// This is  Read-only wrapper class for the StatisticsUserAndTotalMoney view.
    /// </summary>
    [Serializable]
    public partial class StatisticsUserAndTotalMoney : ReadOnlyRecord<StatisticsUserAndTotalMoney>, IReadOnlyRecord
    {
    
	    #region Default Settings
	    protected static void SetSQLProps() 
	    {
		    GetTableSchema();
	    }
	    #endregion
        #region Schema Accessor
	    public static TableSchema.Table Schema
        {
            get
            {
                if (BaseSchema == null)
                {
                    SetSQLProps();
                }
                return BaseSchema;
            }
        }
    	
        private static void GetTableSchema() 
        {
            if(!IsSchemaInitialized)
            {
                //Schema declaration
                TableSchema.Table schema = new TableSchema.Table("StatisticsUserAndTotalMoney", TableType.View, DataService.GetInstance("DataAcessProvider"));
                schema.Columns = new TableSchema.TableColumnCollection();
                schema.SchemaName = @"dbo";
                //columns
                
                TableSchema.TableColumn colvarFullName = new TableSchema.TableColumn(schema);
                colvarFullName.ColumnName = "FullName";
                colvarFullName.DataType = DbType.String;
                colvarFullName.MaxLength = 50;
                colvarFullName.AutoIncrement = false;
                colvarFullName.IsNullable = false;
                colvarFullName.IsPrimaryKey = false;
                colvarFullName.IsForeignKey = false;
                colvarFullName.IsReadOnly = false;
                
                schema.Columns.Add(colvarFullName);
                
                TableSchema.TableColumn colvarSốBàiĐăng = new TableSchema.TableColumn(schema);
                colvarSốBàiĐăng.ColumnName = "Số bài đăng";
                colvarSốBàiĐăng.DataType = DbType.Int32;
                colvarSốBàiĐăng.MaxLength = 0;
                colvarSốBàiĐăng.AutoIncrement = false;
                colvarSốBàiĐăng.IsNullable = true;
                colvarSốBàiĐăng.IsPrimaryKey = false;
                colvarSốBàiĐăng.IsForeignKey = false;
                colvarSốBàiĐăng.IsReadOnly = false;
                
                schema.Columns.Add(colvarSốBàiĐăng);
                
                TableSchema.TableColumn colvarTổngSốLượtView = new TableSchema.TableColumn(schema);
                colvarTổngSốLượtView.ColumnName = "Tổng số lượt view";
                colvarTổngSốLượtView.DataType = DbType.Int32;
                colvarTổngSốLượtView.MaxLength = 0;
                colvarTổngSốLượtView.AutoIncrement = false;
                colvarTổngSốLượtView.IsNullable = false;
                colvarTổngSốLượtView.IsPrimaryKey = false;
                colvarTổngSốLượtView.IsForeignKey = false;
                colvarTổngSốLượtView.IsReadOnly = false;
                
                schema.Columns.Add(colvarTổngSốLượtView);
                
                TableSchema.TableColumn colvarThànhTiền = new TableSchema.TableColumn(schema);
                colvarThànhTiền.ColumnName = "Thành tiền";
                colvarThànhTiền.DataType = DbType.String;
                colvarThànhTiền.MaxLength = 4000;
                colvarThànhTiền.AutoIncrement = false;
                colvarThànhTiền.IsNullable = true;
                colvarThànhTiền.IsPrimaryKey = false;
                colvarThànhTiền.IsForeignKey = false;
                colvarThànhTiền.IsReadOnly = false;
                
                schema.Columns.Add(colvarThànhTiền);
                
                
                BaseSchema = schema;
                //add this schema to the provider
                //so we can query it later
                DataService.Providers["DataAcessProvider"].AddSchema("StatisticsUserAndTotalMoney",schema);
            }
        }
        #endregion
        
        #region Query Accessor
	    public static Query CreateQuery()
	    {
		    return new Query(Schema);
	    }
	    #endregion
	    
	    #region .ctors
	    public StatisticsUserAndTotalMoney()
	    {
            SetSQLProps();
            SetDefaults();
            MarkNew();
        }
        public StatisticsUserAndTotalMoney(bool useDatabaseDefaults)
	    {
		    SetSQLProps();
		    if(useDatabaseDefaults)
		    {
				ForceDefaults();
			}
			MarkNew();
	    }
	    
	    public StatisticsUserAndTotalMoney(object keyID)
	    {
		    SetSQLProps();
		    LoadByKey(keyID);
	    }
    	 
	    public StatisticsUserAndTotalMoney(string columnName, object columnValue)
        {
            SetSQLProps();
            LoadByParam(columnName,columnValue);
        }
        
	    #endregion
	    
	    #region Props
	    
          
        [XmlAttribute("FullName")]
        [Bindable(true)]
        public string FullName 
	    {
		    get
		    {
			    return GetColumnValue<string>("FullName");
		    }
            set 
		    {
			    SetColumnValue("FullName", value);
            }
        }
	      
        [XmlAttribute("SốBàiĐăng")]
        [Bindable(true)]
        public int? SốBàiĐăng 
	    {
		    get
		    {
			    return GetColumnValue<int?>("Số bài đăng");
		    }
            set 
		    {
			    SetColumnValue("Số bài đăng", value);
            }
        }
	      
        [XmlAttribute("TổngSốLượtView")]
        [Bindable(true)]
        public int TổngSốLượtView 
	    {
		    get
		    {
			    return GetColumnValue<int>("Tổng số lượt view");
		    }
            set 
		    {
			    SetColumnValue("Tổng số lượt view", value);
            }
        }
	      
        [XmlAttribute("ThànhTiền")]
        [Bindable(true)]
        public string ThànhTiền 
	    {
		    get
		    {
			    return GetColumnValue<string>("Thành tiền");
		    }
            set 
		    {
			    SetColumnValue("Thành tiền", value);
            }
        }
	    
	    #endregion
    
	    #region Columns Struct
	    public struct Columns
	    {
		    
		    
            public static string FullName = @"FullName";
            
            public static string SốBàiĐăng = @"Số bài đăng";
            
            public static string TổngSốLượtView = @"Tổng số lượt view";
            
            public static string ThànhTiền = @"Thành tiền";
            
	    }
	    #endregion
	    
	    
	    #region IAbstractRecord Members
        public new CT GetColumnValue<CT>(string columnName) {
            return base.GetColumnValue<CT>(columnName);
        }
        public object GetColumnValue(string columnName) {
            return base.GetColumnValue<object>(columnName);
        }
        #endregion
	    
    }
}
