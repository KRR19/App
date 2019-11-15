import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Responsemodel} from '../../../models/Responsemodel';
import {PrintingEditionModel} from '../../../models/PrintingEditionModel';
import {environment} from '../../../../environments/environment';

@Injectable()
export class PrintingEditionService {
  private Api = 'api';
  private control = 'PrintingEdition';
  private ActionCreate = 'Post';
  private ActionGetAll = 'GetAll';
  private ActionGet = 'Get';
  private ActionDelete = 'Delete';
  private ActionUpdate = 'Update';


  constructor(private  http: HttpClient) {
  }

  public async Create(model: PrintingEditionModel): Promise<Responsemodel> {
    console.log(model);
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionCreate;
    const result: Responsemodel = await this.http.post<Responsemodel>(urlPath, model).toPromise();
    return result;
  }

  public async GetAll(): Promise<PrintingEditionModel[]> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionGetAll;
    const result: PrintingEditionModel[] = await this.http.get<PrintingEditionModel[]>(urlPath).toPromise();
    return result;
  }

  public async Get(id: string): Promise<PrintingEditionModel> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionGet + '/';
    const result: PrintingEditionModel = await this.http.get<PrintingEditionModel>(urlPath + id).toPromise();
    return result;
  }

  public async Delete(printingEdition: PrintingEditionModel) {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionDelete;
    const result: Responsemodel = await this.http.post<Responsemodel>(urlPath, printingEdition).toPromise();
    return result;
  }

  public async Update(printingEdition: PrintingEditionModel) {
    console.log(printingEdition.authorId);
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionUpdate;
    const result: Responsemodel = await this.http.put<Responsemodel>(urlPath, printingEdition).toPromise();
    return result;
  }

  public PriceSwitcher(element: number) {
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
