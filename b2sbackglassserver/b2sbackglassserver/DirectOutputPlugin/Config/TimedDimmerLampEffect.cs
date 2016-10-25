using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectOutput.Cab.Toys;

namespace DirectOutput.FX
{
    /// <summary>
    /// The TimedDimmerLampEffect will set the assigned ILampToy to a specific brightness within a specified time span using a dimmer effect.
    /// </summary>
    public class TimedDimmerLampEffect : EffectBase, IEffect
    {
        private string _LampToyName;

        /// <summary>
        /// Name of the <see cref="ILampToy"/>.
        /// </summary>
        /// <value>Name of the <see cref="ILampToy"/>.</value>
        public string LampToyName
        {
            get { return _LampToyName; }
            set
            {
                if (_LampToyName != value)
                {
                    _LampToyName = value;
                    _LampToy = null;
                }
            }
        }

        private void ResolveName(Pinball Pinball)
        {
            if (!LampToyName.IsNullOrWhiteSpace() && Pinball.Cabinet.Toys.Contains(LampToyName))
            {
                if (Pinball.Cabinet.Toys[LampToyName] is ILampToy)
                {
                    _LampToy = (ILampToy)Pinball.Cabinet.Toys[LampToyName];
                }
            }
        }

        private ILampToy _LampToy;

        /// <summary>
        /// Reference to the <see cref="ILampToy"/> specified in the <paramref name="LampToyName"/> property.<br/>
        /// If the  <paramref name="LampToyName"/> property is empty or contains a unknown name or the name of a toy which does not implement <see cref="ILampToy"/> this property will return null.
        /// </summary>
        public ILampToy LampToy
        {
            get
            {
                return _LampToy;
            }
        }

        /// <summary>
        /// Gets or sets the Brightness (0-255) which will be set for the ILampToy if the Brightness of the TableElement supplied to the trigger method is >0.
        /// </summary>
        /// <Brightness>
        /// The Brightness which will be set for the ILampToy if the Brightness of the TableElement supplied to the trigger method is >0.
        /// </Brightness>
        private int _BrightnessOn = 255;

        public int BrightnessOn
        {
            get { return _BrightnessOn; }
            set { _BrightnessOn = value.Limit(0, 255); }
        }

        /// <summary>
        /// Gets or sets the Brightness (0-255) which will be set for the ILampToy if the Brightness of the TableElement supplied to the trigger method is 0.
        /// </summary>
        /// <Brightness>
        /// The Brightness which will be set for the ILampToy if the Brightness of the TableElement supplied to the trigger method is 0.
        /// </Brightness>
        private int _BrightnessOff = 0;

        public int BrightnessOff
        {
            get { return _BrightnessOff; }
            set { _BrightnessOff = value.Limit(0, 255); }
        }

        private int _DimmerDurationMs = 500;

        /// <summary>
        /// Gets or sets the dimmer duration in milliseconds.<br/>
        /// This value specifies the timespan the dimmed lamp will take to reach the target brighness.
        /// </summary>
        /// <value>
        /// The dimmer duration in milliseconds (must be &gt;=20).
        /// </value>
        public int DimmerDurationMs
        {
            get { return _DimmerDurationMs; }
            set { _DimmerDurationMs = value.Limit(20,int.MaxValue); }
        }


        /// <summary>
        /// Triggers the effect.<br/>
        /// If the value of the TableElement is 0 the brightness for the <see cref="ILampToy"/> will be dimmed to the value of the property BrightsOff, if the value of the TableElement is greater than 0 the value of the property BrightnessOn will be used.
        /// If TableElement is null, the brightness of the <see cref="ILampToy"/> will be dimmed to BrightnessOn.
        /// </summary>
        public override void Trigger(DirectOutput.Table.TableElementData TableElementData)
        {
            if (LampToy != null)
            {
                if (TableElementData != null)
                {
                    StartDimmer((TableElementData.Value != 0 ? BrightnessOn : BrightnessOff));
                }
                else
                {
                    StartDimmer(BrightnessOn);
                }
            }
        }

        private UpdateTimer Timer;

        private void StartDimmer(int TargetBrightness)
        {
            if (LampToy != null)
            {
                if (LampToy.Brightness == TargetBrightness) return;
                this.TargetBrightness = TargetBrightness.Limit(0,255);

                StartBrightness = LampToy.Brightness;
                TargetSetTime = DateTime.Now;
            }
        }

        private int TargetBrightness = 0;
        private int StartBrightness = 0;
        private DateTime TargetSetTime;

        private void UpdateDimmer()
        {
            if (TargetBrightness < 0) return;

            if (LampToy != null)
            {
                if (LampToy.Brightness == TargetBrightness) return;

                TimeSpan SinceStart = (DateTime.Now - TargetSetTime);
                double Fract = SinceStart.TotalMilliseconds/DimmerDurationMs;

                if (Fract >= 1)
                {
                    LampToy.SetBrightness(TargetBrightness);
                    TargetBrightness = -1;
                }
                else
                {
                    LampToy.SetBrightness((StartBrightness+(int)((TargetBrightness-StartBrightness)*Fract)).Limit(0,255));
                }
            }
        }



        /// <summary>
        /// Initializes the <see cref="ILampToy"/>.
        /// </summary>
        public override void Init(Pinball Pinball)
        {
            ResolveName(Pinball);

            Timer = Pinball.UpdateTimer;
            Timer.RegisterIntervalAlarm(20, UpdateDimmer);

            if (LampToy != null)
            {
                TargetBrightness = LampToy.Brightness;
                Timer = Pinball.UpdateTimer;
                Timer.RegisterIntervalAlarm(20, UpdateDimmer);
            }
        }
        /// <summary>
        /// Finishes the <see cref="ILampToy"/>.
        /// </summary>
        public override void Finish()
        {

            if (LampToy != null)
            {
                LampToy.Reset();
                if (Timer != null) Timer.UnregisterIntervalAlarm(UpdateDimmer);
            }
                _LampToy = null;
                Timer = null;
        }


    }
}
