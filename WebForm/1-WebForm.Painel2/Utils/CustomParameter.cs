using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Loja.Utils;

namespace Loja.Utils
{
    public class CustomParameterLoja : System.Web.UI.WebControls.Parameter
    {

        #region Constructors

		/// <summary>
		/// The default constructor; creates a new instance of the IdentityParameter object.
		/// </summary>
        public CustomParameterLoja()
		{
		}

		protected CustomParameterLoja(CustomParameterLoja original) : base(original)
		{
			IncludeDomain = original.IncludeDomain;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		/// Determines whether the domain is included in the user name returned by the parameter
		/// </summary>
		[DefaultValue(true)]
		public bool IncludeDomain
		{
			get
			{
				object o = ViewState["IncludeDomain"];
				if (o == null)
					return true;
				else
					return Convert.ToBoolean(o);
			}
			set
			{
				ViewState["IncludeDomain"] = value;
			}
		}

		#endregion Properties
			
		#region Methods

		protected override object Evaluate(HttpContext context, Control control)
        {
            Utils.CustomPrincipal customPrincipal = Utils.Aut.AutenticacaoDados();
            var Name = ViewState["Name"];

            if (customPrincipal != null && customPrincipal.usu_id != 0)
            {
                if (Name.Equals("loj_id"))
                    return customPrincipal.loj_id;
                else if (Name.Equals("usu_id"))
                    return customPrincipal.usu_id;
                if (Name.Equals("usu_nome"))
                    return customPrincipal.usu_nome;
            }
            return null;
        }

		/// <summary>
		/// Creates a clone of the parameter object
		/// </summary>
		/// <remarks>Needs to be provided in order to support design-time parameter editing support.
		/// See http://www.leftslipper.com/ShowFaq.aspx?FaqId=11 for more information.</remarks>
		protected override Parameter Clone()
		{
            return new CustomParameterLoja(this);
		}

		#endregion Methods
	}
}