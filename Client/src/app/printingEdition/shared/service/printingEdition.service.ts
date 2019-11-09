import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Responsemodel} from '../../../models/Responsemodel';
import {PrintingEditionModel} from '../../../models/PrintingEditionModel';

@Injectable()
export class PrintingEditionService {
  constructor(private  http: HttpClient) {}

  public async Create(model: PrintingEditionModel): Promise<Responsemodel> {
    const result: Responsemodel  = await this.http.post<Responsemodel>('https://localhost:44378/api/PrintingEdition/Post', model).toPromise();
    return result;
  }

  public async GetAll(): Promise<PrintingEditionModel[]> {
    const result: PrintingEditionModel[] = await this.http.get<PrintingEditionModel[]>('https://localhost:44378/api/PrintingEdition/GetAll').toPromise();
    return result;
  }

  public async Get(id: string): Promise<PrintingEditionModel> {
    const result: PrintingEditionModel = await this.http.get<PrintingEditionModel>('https://localhost:44378/api/PrintingEdition/Get/' + id).toPromise();
    return result;
  }

  public async delete(printingEdition: PrintingEditionModel) {
    const result: Responsemodel = await this.http.post<Responsemodel>('https://localhost:44378/api/PrintingEdition/Delete', printingEdition).toPromise();
    return result;
  }

  async Update(printingEdition: PrintingEditionModel) {
    const result: Responsemodel = await this.http.put<Responsemodel>('https://localhost:44378/api/PrintingEdition/Update', printingEdition).toPromise();
    return result;
  }

  PriceSwitcher(element: number) {
    switch (element) {
      case 1:
        return 'USD';
      case 2:
        return 'EUR';
      case 3:
        return 'UAH';
    }
  }


}
