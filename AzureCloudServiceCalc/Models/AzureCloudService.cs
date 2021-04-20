using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCloudServiceCalc.Models
{
    public class AzureCloudService
        {
            // instance size descriptions for a cloud service
            public static String[] InstanceSizeDescriptions
            {
                get
                {
                    return new String[] { "Very Small", "Small", "Medium", "Large", "Very Large", "A5", "A6" };
                }
            }

            // corresponding prices per hour ($) for each instance size as above
            public static double[] InstanceSizePrices
            {
                get
                {
                    return new double[] { 0.02, 0.08, 0.16, 0.32, 0.64, 0.90, 1.80 };
                }
            }
        // no of instances for a service
        [Required(ErrorMessage = "Required field!")]
        [Range(2, Int32.MaxValue, ErrorMessage = "At least 2 instances required")]
        [DisplayName("No of Instances")]
        public int NoInstances { get; set; }

        // size of an instance e.g. very small, small etc
        [Required(ErrorMessage = "Required field!")]
        [DisplayName("Instance Size")]
        public String InstanceSize { get; set; }
        // get the cost of a service based on #instances and size for a year
        public double Cost
        {
            get
            {
                int size = 0;
                for (int i = 0; i < AzureCloudService.InstanceSizeDescriptions.Length; i++)
                {
                    if (AzureCloudService.InstanceSizeDescriptions[i] == this.InstanceSize)
                    {
                        size = i;
                        break;
                    }
                }
                double hourlyPrice = NoInstances * InstanceSizePrices[size];
                double dailyPrice = hourlyPrice * 24;
                double yearlyPrice;

                if (DateTime.IsLeapYear(DateTime.Now.Year))
                {
                    yearlyPrice = dailyPrice * 366;
                }
                else
                {
                    yearlyPrice = dailyPrice * 365;
                }
                return yearlyPrice;
            }
        }
    }
}
