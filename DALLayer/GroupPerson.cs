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
namespace SweetCMS.DataAccess
{
	/// <summary>
	/// Strongly-typed collection for the GroupPerson class.
	/// </summary>
    [Serializable]
	public partial class GroupPersonCollection : ActiveList<GroupPerson, GroupPersonCollection>
	{	   
		public GroupPersonCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>GroupPersonCollection</returns>
		public GroupPersonCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                GroupPerson o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the Group_People table.
	/// </summary>
	[Serializable]
	public partial class GroupPerson : ActiveRecord<GroupPerson>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public GroupPerson()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public GroupPerson(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public GroupPerson(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public GroupPerson(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("Group_People", TableType.Table, DataService.GetInstance("DataAcessProvider"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"dbo";
				//columns
				
				TableSchema.TableColumn colvarId = new TableSchema.TableColumn(schema);
				colvarId.ColumnName = "Id";
				colvarId.DataType = DbType.Int32;
				colvarId.MaxLength = 0;
				colvarId.AutoIncrement = true;
				colvarId.IsNullable = false;
				colvarId.IsPrimaryKey = true;
				colvarId.IsForeignKey = false;
				colvarId.IsReadOnly = false;
				colvarId.DefaultSetting = @"";
				colvarId.ForeignKeyTableName = "";
				schema.Columns.Add(colvarId);
				
				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "Name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = false;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				
						colvarName.DefaultSetting = @"('Nguy?n Van A')";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);
				
				TableSchema.TableColumn colvarDescription = new TableSchema.TableColumn(schema);
				colvarDescription.ColumnName = "Description";
				colvarDescription.DataType = DbType.String;
				colvarDescription.MaxLength = 200;
				colvarDescription.AutoIncrement = false;
				colvarDescription.IsNullable = false;
				colvarDescription.IsPrimaryKey = false;
				colvarDescription.IsForeignKey = false;
				colvarDescription.IsReadOnly = false;
				colvarDescription.DefaultSetting = @"";
				colvarDescription.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDescription);
				
				TableSchema.TableColumn colvarActive = new TableSchema.TableColumn(schema);
				colvarActive.ColumnName = "Active";
				colvarActive.DataType = DbType.Boolean;
				colvarActive.MaxLength = 0;
				colvarActive.AutoIncrement = false;
				colvarActive.IsNullable = true;
				colvarActive.IsPrimaryKey = false;
				colvarActive.IsForeignKey = false;
				colvarActive.IsReadOnly = false;
				
						colvarActive.DefaultSetting = @"((1))";
				colvarActive.ForeignKeyTableName = "";
				schema.Columns.Add(colvarActive);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["DataAcessProvider"].AddSchema("Group_People",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Id")]
		[Bindable(true)]
		public int Id 
		{
			get { return GetColumnValue<int>(Columns.Id); }
			set { SetColumnValue(Columns.Id, value); }
		}
		  
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get { return GetColumnValue<string>(Columns.Name); }
			set { SetColumnValue(Columns.Name, value); }
		}
		  
		[XmlAttribute("Description")]
		[Bindable(true)]
		public string Description 
		{
			get { return GetColumnValue<string>(Columns.Description); }
			set { SetColumnValue(Columns.Description, value); }
		}
		  
		[XmlAttribute("Active")]
		[Bindable(true)]
		public bool? Active 
		{
			get { return GetColumnValue<bool?>(Columns.Active); }
			set { SetColumnValue(Columns.Active, value); }
		}
		
		#endregion
		
		
		#region PrimaryKey Methods		
		
        protected override void SetPrimaryKey(object oValue)
        {
            base.SetPrimaryKey(oValue);
            
            SetPKValues();
        }
        
		
		private SweetCMS.DataAccess.GroupPeopleDetailCollection colGroupPeopleDetailRecords;
		public SweetCMS.DataAccess.GroupPeopleDetailCollection GroupPeopleDetailRecords()
		{
			if(colGroupPeopleDetailRecords == null)
			{
				colGroupPeopleDetailRecords = new SweetCMS.DataAccess.GroupPeopleDetailCollection().Where(GroupPeopleDetail.Columns.GroupPeopleId, Id).Load();
				colGroupPeopleDetailRecords.ListChanged += new ListChangedEventHandler(colGroupPeopleDetailRecords_ListChanged);
			}
			return colGroupPeopleDetailRecords;
		}
				
		void colGroupPeopleDetailRecords_ListChanged(object sender, ListChangedEventArgs e)
		{
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
		        // Set foreign key value
		        colGroupPeopleDetailRecords[e.NewIndex].GroupPeopleId = Id;
            }
		}
		#endregion
		
			
		
		//no foreign key tables defined (0)
		
		
		
		#region Many To Many Helpers
		
		 
		public SweetCMS.DataAccess.UserCollection GetUserCollection() { return GroupPerson.GetUserCollection(this.Id); }
		public static SweetCMS.DataAccess.UserCollection GetUserCollection(int varId)
		{
		    SubSonic.QueryCommand cmd = new SubSonic.QueryCommand("SELECT * FROM [dbo].[Users] INNER JOIN [Group_People_Detail] ON [Users].[Id] = [Group_People_Detail].[UserId] WHERE [Group_People_Detail].[GroupPeopleId] = @GroupPeopleId", GroupPerson.Schema.Provider.Name);
			cmd.AddParameter("@GroupPeopleId", varId, DbType.Int32);
			IDataReader rdr = SubSonic.DataService.GetReader(cmd);
			UserCollection coll = new UserCollection();
			coll.LoadAndCloseReader(rdr);
			return coll;
		}
		
		public static void SaveUserMap(int varId, UserCollection items)
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Group_People_Detail] WHERE [Group_People_Detail].[GroupPeopleId] = @GroupPeopleId", GroupPerson.Schema.Provider.Name);
			cmdDel.AddParameter("@GroupPeopleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (User item in items)
			{
				GroupPeopleDetail varGroupPeopleDetail = new GroupPeopleDetail();
				varGroupPeopleDetail.SetColumnValue("GroupPeopleId", varId);
				varGroupPeopleDetail.SetColumnValue("UserId", item.GetPrimaryKeyValue());
				varGroupPeopleDetail.Save();
			}
		}
		public static void SaveUserMap(int varId, System.Web.UI.WebControls.ListItemCollection itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Group_People_Detail] WHERE [Group_People_Detail].[GroupPeopleId] = @GroupPeopleId", GroupPerson.Schema.Provider.Name);
			cmdDel.AddParameter("@GroupPeopleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (System.Web.UI.WebControls.ListItem l in itemList) 
			{
				if (l.Selected) 
				{
					GroupPeopleDetail varGroupPeopleDetail = new GroupPeopleDetail();
					varGroupPeopleDetail.SetColumnValue("GroupPeopleId", varId);
					varGroupPeopleDetail.SetColumnValue("UserId", l.Value);
					varGroupPeopleDetail.Save();
				}
			}
		}
		public static void SaveUserMap(int varId , int[] itemList) 
		{
			QueryCommandCollection coll = new SubSonic.QueryCommandCollection();
			//delete out the existing
			 QueryCommand cmdDel = new QueryCommand("DELETE FROM [Group_People_Detail] WHERE [Group_People_Detail].[GroupPeopleId] = @GroupPeopleId", GroupPerson.Schema.Provider.Name);
			cmdDel.AddParameter("@GroupPeopleId", varId, DbType.Int32);
			coll.Add(cmdDel);
			DataService.ExecuteTransaction(coll);
			foreach (int item in itemList) 
			{
				GroupPeopleDetail varGroupPeopleDetail = new GroupPeopleDetail();
				varGroupPeopleDetail.SetColumnValue("GroupPeopleId", varId);
				varGroupPeopleDetail.SetColumnValue("UserId", item);
				varGroupPeopleDetail.Save();
			}
		}
		
		public static void DeleteUserMap(int varId) 
		{
			QueryCommand cmdDel = new QueryCommand("DELETE FROM [Group_People_Detail] WHERE [Group_People_Detail].[GroupPeopleId] = @GroupPeopleId", GroupPerson.Schema.Provider.Name);
			cmdDel.AddParameter("@GroupPeopleId", varId, DbType.Int32);
			DataService.ExecuteQuery(cmdDel);
		}
		
		#endregion
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varName,string varDescription,bool? varActive)
		{
			GroupPerson item = new GroupPerson();
			
			item.Name = varName;
			
			item.Description = varDescription;
			
			item.Active = varActive;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(int varId,string varName,string varDescription,bool? varActive)
		{
			GroupPerson item = new GroupPerson();
			
				item.Id = varId;
			
				item.Name = varName;
			
				item.Description = varDescription;
			
				item.Active = varActive;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn IdColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn DescriptionColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ActiveColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Id = @"Id";
			 public static string Name = @"Name";
			 public static string Description = @"Description";
			 public static string Active = @"Active";
						
		}
		#endregion
		
		#region Update PK Collections
		
        public void SetPKValues()
        {
                if (colGroupPeopleDetailRecords != null)
                {
                    foreach (SweetCMS.DataAccess.GroupPeopleDetail item in colGroupPeopleDetailRecords)
                    {
                        if (item.GroupPeopleId != Id)
                        {
                            item.GroupPeopleId = Id;
                        }
                    }
               }
		}
        #endregion
    
        #region Deep Save
		
        public void DeepSave()
        {
            Save();
            
                if (colGroupPeopleDetailRecords != null)
                {
                    colGroupPeopleDetailRecords.SaveAll();
               }
		}
        #endregion
	}
}
