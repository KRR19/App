import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ExchangeService {
  private EurUsdRates = 1.1;
  private UsdUahRates = 25;

  constructor() {
  }

  public EurUsd(current: number) {
    return this.EurUsdRates * current;
  }

  public UsdEur(current: number) {
    return current / this.EurUsdRates;
  }

  public UsdUah(current: number) {
    return current / this.UsdUahRates;
  }

  public UahUsd(current: number) {
    return current * this.UsdUahRates;
  }
}
