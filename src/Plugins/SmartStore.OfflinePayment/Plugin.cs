using System;
using SmartStore.Core.Plugins;
using SmartStore.Services.Localization;
using SmartStore.Services.Configuration;
using SmartStore.OfflinePayment.Settings;
using SmartStore.Services;

namespace SmartStore.OfflinePayment
{

	public partial class Plugin : BasePlugin
    {
		private readonly ICommonServices _services;

		public Plugin(ICommonServices services)
        {
			this._services = services;
        }

        public override void Install()
        {
			var settings = _services.Settings;
			var loc = _services.Localization;
			
			// add settings
			settings.SaveSetting(new CashOnDeliveryPaymentSettings
			{
				DescriptionText = "@Plugins.Payment.CashOnDelivery.PaymentInfoDescription"
			});

			// add resources
			loc.ImportPluginResourcesFromXml(this.PluginDescriptor);
            
            base.Install();
        }

        public override void Uninstall()
        {
			var settings = _services.Settings;
			var loc = _services.Localization;

			// delete settings
			settings.DeleteSetting<CashOnDeliveryPaymentSettings>();

			// delete resources
			loc.DeleteLocaleStringResources(this.PluginDescriptor.ResourceRootKey);
			loc.DeleteLocaleStringResources("Plugins.Payment.CashOnDelivery");
			loc.DeleteLocaleStringResources("Plugins.Payments.DirectDebit");
			loc.DeleteLocaleStringResources("Plugins.Payment.Invoice");
			loc.DeleteLocaleStringResources("Plugins.Payments.Manual");
			loc.DeleteLocaleStringResources("Plugins.Payment.PayInStore");
			loc.DeleteLocaleStringResources("Plugins.Payment.Prepayment");

            base.Uninstall();
        }
    }
}