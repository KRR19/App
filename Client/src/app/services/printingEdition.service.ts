import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ResponseModel} from '../shared/models/response.model';
import {PrintingEditionModel} from '../shared/models/printing-edition.model';
import {environment} from '../../environments/environment';
import {FilterModel} from '../shared/models/filter.model';

@Injectable()
export class PrintingEditionService {
  private api = 'api';
  private control = 'PrintingEdition';
  private actionCreate = 'Post';
  private actionGetAll = 'GetAll';
  private actionGet = 'Get';
  private actionDelete = 'Delete';
  private actionUpdate = 'Update';
  private actionFilter = 'Filter';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll(): Promise<PrintingEditionModel[]> {
    const urlPath = `${this.serverUrl}/${this.actionGetAll}`;
    const result: PrintingEditionModel[] = await this.http.get<PrintingEditionModel[]>(urlPath).toPromise();
    return result;
  }

  public async Get(id: string): Promise<PrintingEditionModel> {
    const urlPath = `${this.serverUrl}/${this.actionGet}/`;
    const result: PrintingEditionModel = await this.http.get<PrintingEditionModel>(urlPath + id).toPromise();
    return result;
  }

  public async Create(model: PrintingEditionModel): Promise<ResponseModel> {
    const urlPath = `${this.serverUrl}/${this.actionCreate}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, model).toPromise();
    return result;
  }

  public async Filter(model: FilterModel): Promise<PrintingEditionModel> {
    debugger;
    const urlPath = `${this.serverUrl}/${this.actionFilter}`;
    const result: PrintingEditionModel = await this.http.post<PrintingEditionModel>(urlPath, model).toPromise();
    return result;
  }

  public async Delete(printingEdition: PrintingEditionModel) {
    const urlPath = `${this.serverUrl}/${this.actionDelete}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, printingEdition).toPromise();
    return result;
  }

  public async Update(printingEdition: PrintingEditionModel) {
    const urlPath = `${this.serverUrl}/${this.actionUpdate}`;
    const result: ResponseModel = await this.http.post<ResponseModel>(urlPath, printingEdition).toPromise();
    return result;
  }
}
