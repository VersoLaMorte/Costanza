﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Extensions to UrlHelper for manipulating querystrings.
    /// </summary>
    public static class QueryStringExtensionsForUrlHelper
    {
        /// <summary>
        /// Remove the specified parameters from the URL of the current request.
        /// </summary>
        /// <param name="helper">The UrlHelper that contains the current request/URL.</param>
        /// <param name="parameters">
        /// The names of the parameters that will be removed from the query string.
        /// </param>
        /// <returns>The current URL, including querystring, without the removed parameters.</returns>
        public static string RemoveParameters( this UrlHelper helper, params string[] parameters )
        {
            var queryString = HttpUtility.ParseQueryString( helper.RequestContext.HttpContext.Request.Url.Query );
            foreach( var p in parameters )
            {
                queryString.Remove( p );
            }

            return string.Concat( helper.Action( "" ), queryString.Count == 0 ? "" : "?", queryString );
        }

        /// <summary>
        /// Adds the specified parameters to the URL of the current request.
        /// </summary>
        /// <param name="helper">The UrlHelper that contains the current request/URL.</param>
        /// <param name="parameters">An object whose name-value property pairs will be added to the querystring.</param>
        /// <returns>The current URL, including querystring, with the newly added parameters.</returns>
        public static string SetParameters( this UrlHelper helper, object parameters )
        {
            return helper.SetParameters( new RouteValueDictionary( parameters ) );
        }

        /// <summary>
        /// Adds the specified parameters to the URL of the current request.
        /// </summary>
        /// <param name="helper">The UrlHelper that contains the current request/URL.</param>
        /// <param name="parameters">A dictionary whose name-value property pairs will be added to the querystring.</param>
        /// <returns>The current URL, including querystring, with the newly added parameters.</returns>
        public static string SetParameters( this UrlHelper helper, RouteValueDictionary parameters )
        {
            var queryString = HttpUtility.ParseQueryString( helper.RequestContext.HttpContext.Request.Url.Query );
            foreach( var p in parameters )
            {
                if( ( p.Value as string ).IsBlank() )
                {
                    queryString.Remove( p.Key );
                    continue;
                }

                queryString[ p.Key.ToLowerInvariant() ] = p.Value == null ? null : p.Value.ToString();
            }

            return string.Concat( helper.Action( "" ), queryString.Count == 0 ? "" : "?", queryString );
        }
    }
}