using EGO.Gladius.DataTypes;
using EGO.Gladius.Extensions;

SPR<int> res = await SPR.FromResult(1).To(Transform).To(async x => await Transform(x));




Console.ReadLine();

async Task<int> Transform(int x)
{
    await Task.Delay(1000);

    return x + 2;
}

