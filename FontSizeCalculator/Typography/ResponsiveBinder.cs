namespace FontSizeCalculator.Typography
{
    public static class ResponsiveBinder
    {
        // Binds a label to recompute font when the container is laid out.
        // - Applies immediately with fallback size if container is 0×0
        // - Debounces SizeChanged
        public static void Bind(Label label, TextConfig cfg, VisualElement container, Action<Label, TextConfig, double, double> applyFit)
        {
            // 1) Apply immediately with a safe fallback
            var (w, h) = GetInitialSize(container);
            applyFit(label, cfg, w, h);

            // 2) Debounced re-apply on size changes
            bool pending = false;
            container.SizeChanged += (_, __) =>
            {
                if (container.Width <= 0 || container.Height <= 0) return;
                if (pending) return;
                pending = true;

                // Wait one frame to let layout settle
                Device.StartTimer(TimeSpan.FromMilliseconds(16), () =>
                {
                    pending = false;
                    var cw = Math.Max(1, container.Width);
                    var ch = Math.Max(1, container.Height);
                    applyFit(label, cfg, cw, ch);
                    return false;
                });
            };

            // 3) As a fallback, also listen to container LayoutChanged (fires in more cases)
            // container.LayoutChanged += (_, __) =>
            // {
            //     if (container.Width <= 0 || container.Height <= 0) return;
            //     var cw = Math.Max(1, container.Width);
            //     var ch = Math.Max(1, container.Height);
            //     applyFit(label, cfg, cw, ch);
            // };
        }

        private static (double w, double h) GetInitialSize(VisualElement container)
        {
            if (container.Width > 0 && container.Height > 0)
                return (container.Width, container.Height);

            // Fallback to window size in dp
            var info = DeviceDisplay.Current.MainDisplayInfo;
            var w = Math.Max(1, info.Width / info.Density);
            var h = Math.Max(1, info.Height / info.Density);
            return (w, h);
        }
    }
}
