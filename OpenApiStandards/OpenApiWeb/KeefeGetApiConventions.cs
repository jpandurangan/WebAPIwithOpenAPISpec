using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpenApiWeb
{

    //https://raw.githubusercontent.com/aspnet/Mvc/master/src/Microsoft.AspNetCore.Mvc.Core/DefaultApiConventions.cs

    public static class KeefeGetApiConventions
    {
        #region GET

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public static void Get()
        {
        }

        #endregion
    }
}
