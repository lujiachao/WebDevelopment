using App.Metrics.Health;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppMetricsTest
{
    public class OKHealthCheck : HealthCheck
    {
        public OKHealthCheck() : base("正常的检查(OKHealthCheck)") { }

        protected override Task<HealthCheckResult> CheckAsync(CancellationToken token = default(CancellationToken))
        {
            int second = DateTime.Now.Second;
            switch (second%3)
            {
                case 0:
                    return Task.FromResult(HealthCheckResult.Healthy("OK"));
                case 1:
                    return Task.FromResult(HealthCheckResult.Degraded("Degraded"));
                default:
                    return Task.FromResult(HealthCheckResult.Unhealthy("不健康"));
            }
        }
    }
}
