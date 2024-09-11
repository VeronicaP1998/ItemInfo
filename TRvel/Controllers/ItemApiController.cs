using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using TRvel;

namespace travel.Controllers
{
    [RoutePrefix("api/ItemApi")]

    public class ItemApiController : ApiController
    {
        TravelUpEntities1 db = new TravelUpEntities1();

        /// <summary>
        /// to get all items on click of itemlist button
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetItemList")]
        public IHttpActionResult GetItemList()
        {
            try
            {
                using (db = new TravelUpEntities1())
                {
                    var itemlist = db.tblItems.ToList();
                    return Ok(itemlist);
                }
            }
            catch (SqlException sqlexcep)
            {
                throw (sqlexcep);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// to delete single item on click of delete button
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IHttpActionResult DeleteItem(int id)
        {
            try
            {
                using (db = new TravelUpEntities1())
                {
                    var result = db.tblItems.Where(i => i.ID == id).FirstOrDefault();
                    if (result != null)
                    {
                        db.tblItems.Remove(result);
                        db.SaveChanges();
                        return Ok(result);
                    }
                }
                return Ok(HttpStatusCode.NoContent);
            }
            catch (SqlException sqlexcep)
            {
                throw (sqlexcep);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// to create or update item details on click of submit button
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ItemCreation")]
        public IHttpActionResult ItemCreation([FromBody] tblItem orders)
        {
            try
            {
                using (TravelUpEntities1 db = new TravelUpEntities1())
                {
                    if (orders.ID > 0)
                    {
                        var updateitem = db.tblItems.Where(i => i.ID == orders.ID).FirstOrDefault();
                        updateitem.ItemName = orders.ItemName;
                        updateitem.Quantity = orders.Quantity;
                        updateitem.Price = orders.Price;
                        db.Entry(updateitem).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        tblItem orderItem = new tblItem();
                        orderItem.ItemName = orders.ItemName;
                        orderItem.Quantity = orders.Quantity;
                        orderItem.Price = orders.Price;
                        db.tblItems.Add(orderItem);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true, item = orders });
            }
            catch (SqlException sqlexcep)
            {
                throw (sqlexcep);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// to get single item on click of delete or update
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetItemByID/{ID}")]
        public IHttpActionResult GetItemByID(int ID)
        {
            try
            {


                using (TravelUpEntities1 db = new TravelUpEntities1())
                {
                    var result = db.tblItems.Where(i => i.ID == ID).FirstOrDefault();
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                return Ok(HttpStatusCode.NoContent);
            }
            catch (SqlException sqlexcep)
            {
                throw (sqlexcep);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}