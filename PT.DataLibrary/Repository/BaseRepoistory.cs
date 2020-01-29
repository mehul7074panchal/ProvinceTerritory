using PT.DataLibrary.Contract;
using PT.DataLibrary.Data;
using PT.Model.CommonModelLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace PT.DataLibrary.Repository
{
    public class BaseRepoistory<T> : IReposatory<T>
    {
        #region Private Variable
        private readonly string DataEntityNameSpace = "PT.DataLibrary.Data";
        private readonly string DataEntityAssemblyName = "PT.DataLibrary";
        private readonly PTContext _dbContext;

        #endregion

        #region Constructor
        public BaseRepoistory()
        {
            _dbContext = new PTContext();
        }
        #endregion

        #region CRUD Operations
        public int? Delete(T TModel)
        {
            var objArraryModalParams = GetModalWithParameters(TModel,"DELETE");

            if (objArraryModalParams.Length == 0)
                return null;
          
            return ((IObjectContextAdapter)_dbContext).ObjectContext.ExecuteFunction("sp_" + (objArraryModalParams[0] as ModelAttribute).TableName + "_delete", 
                (objArraryModalParams[1] as List<ObjectParameter>).ToArray());

        }

        public int? Insert(T TModel)
        {
            var objArraryModalParams = GetModalWithParameters(TModel,"INSERT");

            if (objArraryModalParams.Length == 0)
                return null;

            var objModelAttribute = objArraryModalParams[0] as ModelAttribute;
            var objectParameters = objArraryModalParams[1] as List<ObjectParameter>;

            if (objModelAttribute.HasAutoID)
            {
                var identity = new ObjectParameter("IDENTITY", SqlDbType.Int)
                {

                  
                    Value = 0
                };
                objectParameters.Add(identity);
            }
           

          

            ((IObjectContextAdapter)_dbContext).ObjectContext.ExecuteFunction("sp_" + objModelAttribute.TableName + "_insert", objectParameters.ToArray());

            return Convert.ToInt32(objectParameters.Find(m => m.Name == "IDENTITY").Value);


        }

        public List<T> SelectAll(T TModel)
        {
            var objArraryModalParams = GetModalWithParameters(TModel,"SELECT");

            if (objArraryModalParams.Length == 0)
                return new List<T>();

            var objModelAttribute = objArraryModalParams[0] as ModelAttribute;
            var objectParameters = objArraryModalParams[1] as List<ObjectParameter>;




            Type typeArgument = Type.GetType(DataEntityNameSpace + ".sp_" + objModelAttribute.TableName + "_select_Result, " + DataEntityAssemblyName);

            Type genericClass = typeof(Generic<>);
            // MakeGenericType is badly named
            Type constructedClass = genericClass.MakeGenericType(typeArgument);


            object created = Activator.CreateInstance(constructedClass, objModelAttribute, objectParameters,_dbContext);

            JArray ResultArray = new JArray();

            foreach (var prpertyinfo in constructedClass.GetRuntimeProperties())
            {


                ResultArray = (JArray)prpertyinfo.GetValue(created, null);

            }

            return ResultArray.Count > 0 ? JsonConvert.DeserializeObject<List<T>>(ResultArray[0].ToString()) : new List<T>();

        }

        public int? Update(T TModel)
        {
            var objArraryModalParams = GetModalWithParameters(TModel,"UPDATE");

            if (objArraryModalParams.Length == 0)
                return null;


            return ((IObjectContextAdapter)_dbContext).ObjectContext.ExecuteFunction("sp_" + (objArraryModalParams[0] as ModelAttribute).TableName + "_update",
                (objArraryModalParams[1] as List<ObjectParameter>).ToArray());

        }

        #endregion


        /// <summary>
        /// Common Process for any Object(Model) and Objects's Property
        /// </summary>
        /// <param name="TModel"></param>
        /// <returns>arrar of ModelAttribute and List<ObjectParameter> </returns>
        public object[] GetModalWithParameters(T TModel,string OperationName) {

            var allModelAttributes = TModel.GetType().GetCustomAttributes(true);

            ModelAttribute objModelAttribute = null;
            foreach (var attribute in allModelAttributes)
            {
                if (attribute.GetType() == typeof(ModelAttribute))
                {
                    objModelAttribute = attribute as ModelAttribute;
                    break;
                }
            }
            if (objModelAttribute == null)
                return  new object[] { };

            //To generate the list of  SqlParameters
            var objectParameters = new List<ObjectParameter>();
            foreach (var objModelProperty in TModel.GetType().GetProperties())
            {
                var allPropertyAttributes = objModelProperty.GetCustomAttributes(true);

                ModelPropertyAttribute objModelPropertyAttribute = null;
                foreach (var attribute in allPropertyAttributes)
                {
                    if (attribute.GetType() == typeof(ModelPropertyAttribute))
                    {
                        objModelPropertyAttribute = attribute as ModelPropertyAttribute;
                        break;
                    }
                }
               

                var operationFlg = objModelPropertyAttribute != null
                    && (OperationName.Equals("DELETE") == objModelPropertyAttribute.IsDeleteParameter 
                    || OperationName.Equals("INSERT") == objModelPropertyAttribute.IsInsertParameter 
                    || OperationName.Equals("SELECT") == objModelPropertyAttribute.IsSelectParameter
                    || OperationName.Equals("UPDATE") == objModelPropertyAttribute.IsUpdateParameter);


                if (operationFlg)
                {
                    var proValue = objModelProperty.GetValue(TModel, new object[] { });
                    var proTypeFullName = objModelProperty.PropertyType.FullName;
                    var proType = Type.GetType((proTypeFullName.Contains("System.Nullable") && proTypeFullName.Split(new string[] { "[["} ,StringSplitOptions.RemoveEmptyEntries).Length > 1) ?
                        proTypeFullName.Split(new string[] { "[[" }, StringSplitOptions.RemoveEmptyEntries)[1].Split(',')[0] : proTypeFullName);

                    objectParameters.Add(proValue != null ?
                   new ObjectParameter(objModelPropertyAttribute.ParameterName, proValue) :
                    new ObjectParameter(objModelPropertyAttribute.ParameterName, proType));

                }
            }

            return new object[] { objModelAttribute, objectParameters };
        }      

        

       

    }

    /// <summary>
    /// Fetch a sp result list genericly  containing records  from Table corresponding to the Business Object.
    /// </summary>
    /// <typeparam name="U">sp reusult object</typeparam>
    public class Generic<U>
    {
        /// <summary>
        ///  By this constructer iniciate class object and call sp. The sp Result is converted in Json Array.
        /// </summary>
        /// <param name="objModelAttribute">detila of table corresponding to the Business Object.</param>
        /// <param name="objectParameters">list all parameters corresponding to the Business Object.</param>
        /// <param name="_dbContext">Database Context refference </param>
        public Generic(ModelAttribute objModelAttribute, List<ObjectParameter> objectParameters, PTContext _dbContext)
        {


            var list = ((IObjectContextAdapter)_dbContext)
                 .ObjectContext
                 .ExecuteFunction<U>
                           ("sp_" + objModelAttribute.TableName + "_select", objectParameters.ToArray()).ToList();


            var josnArry = JsonConvert.SerializeObject(list);
            Jsonarr = new JArray
            {
                josnArry

            };

        }

        private JArray Jsonarr { get; set; }


    }
}
