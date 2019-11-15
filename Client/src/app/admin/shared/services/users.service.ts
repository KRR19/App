import { Injectable } from '@angular/core';
import {PrintingEditionModel} from '../../../models/PrintingEditionModel';
import {environment} from '../../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private Api = 'api';
  private control = 'PrintingEdition';
  private ActionGetAll = 'GetAll';
  private ActionUpdate = 'Update';

  constructor(private  http: HttpClient) { }

  public async GetAll() { // : Promise<User[]> {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionGetAll;
    const result: PrintingEditionModel[] = await this.http.get<PrintingEditionModel[]>(urlPath).toPromise();
    return result;
  }


}
