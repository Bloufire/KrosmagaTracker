using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AddOn_Krosmaga___Blou_fire.Ease
{
    class CustomLinearEasingFunction :EasingFunctionBase
    {
	    public CustomLinearEasingFunction()
		    : base()
	    {
	    }

	    // Specify your own logic for the easing function by overriding
	    // the EaseInCore method. Note that this logic applies to the "EaseIn"
	    // mode of interpolation. 
	    protected override double EaseInCore(double normalizedTime)
	    {
		   
		    return Math.Pow(normalizedTime, 1);
	    }

	    // Typical implementation of CreateInstanceCore
	    protected override Freezable CreateInstanceCore()
	    {

		    return new CustomLinearEasingFunction();
	    }

	}
}
