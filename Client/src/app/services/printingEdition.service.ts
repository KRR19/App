import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../shared/models/response.model';
import {PrintingEditionModel} from '../shared/models/printing-edition.model';
import {environment} from '../../environments/environment';

@Injectable()
export class PrintingEditionService {
  private Api = 'api';
  private control = 'PrintingEdition';
  private ActionCreate = 'Post';
  private ActionGetAll = 'GetAll';
  private ActionGet = 'Get';
  private ActionDelete = 'Delete';
  private ActionUpdate = 'Update';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll(): Promise<PrintingEditionModel[]> {
    const urlPath = `${this.serverUrl}/${this.ActionGetAll}`;
    const result: PrintingEditionModel[] = await this.http.get<PrintingEditionModel[]>(urlPath).toPromise();
    return result;
  }

  public async Get(id: string): Promise<PrintingEditionModel> {
    const urlPath = `${this.serverUrl}/${this.ActionGet}/`;
    const result: PrintingEditionModel = await this.http.get<PrintingEditionModel>(urlPath + id).toPromise();
    return result;
  }

  public async Create(model: PrintingEditionModel): Promise<ResponseModel> {
    const urlPath = `${this.serverUrl}/${this.ActionCreate}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, model).toPromise();
    return result;
  }

  public async Delete(printingEdition: PrintingEditionModel) {
    const urlPath = `${this.serverUrl}/${this.ActionDelete}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, printingEdition).toPromise();
    return result;
  }

  public async Update(printingEdition: PrintingEditionModel) {
    const urlPath = `${this.serverUrl}/${this.ActionUpdate}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, printingEdition).toPromise();
    return result;
  }
}
