import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {
  private EurUsdRates = 1.1;
  private UsdUahRates = 25;

  constructor() { }

  public EurUsd(current: number) {
    console.log('EURUSD: ' + this.EurUsdRates * current);
    return this.EurUsdRates * current;
  }

  public UsdEur(current: number) {
    console.log('USDEUR: ' + this.EurUsdRates * current);
    return current / this.EurUsdRates;
  }

  public UsdUah(current: number) {
    console.log('USDUAH: ' + current / this.UsdUahRates);
    return current / this.UsdUahRates ;
  }
  public UahUsd(current: number) {
    console.log(current);
    console.log('UAHUSD: ' + this.UsdUahRates * current);
    return current * this.UsdUahRates;
  }
}
