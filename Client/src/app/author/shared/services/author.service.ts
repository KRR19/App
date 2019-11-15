import {Injectable} from '@angular/core';
import {AuthorModel} from '../../../models/AuthorModel';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../../environments/environment';

@Injectable()
export class AuthorService {
  private Api = 'api';
  private control = 'Author';
  private ActionGetAll = 'GetAll';
  private ActionCreate = 'Create';
  private ActionGet = 'Get';
  private ActionDelete = 'Delete';
  private ActionUpdate = 'Update';

  constructor(private  http: HttpClient) {
  }

  public async AddAuthor(model: AuthorModel) {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionCreate;
    console.log(model);
    const result: AuthorModel = await this.http.post<AuthorModel>(urlPath, model).toPromise();
    console.log(result);
    return result;
  }

  public async GetAll() {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionGetAll;
    const authors: AuthorModel[] = await this.http.get<AuthorModel[]>(urlPath).toPromise();
    return authors;
  }

  public async GetById(author: string) {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionGet + '/';
    const authors: AuthorModel = await this.http.get<AuthorModel>(urlPath + author).toPromise();
    console.log(authors);
    return authors;
  }

  public async Delete(model: AuthorModel) {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionDelete;
    await this.http.post(urlPath, model).toPromise();
  }

  async EditAuthor(model: AuthorModel) {
    const urlPath: string = environment.protocol + '://' + environment.host + ':' + environment.port + '/' + this.Api + '/' + this.control + '/' + this.ActionUpdate;
    console.log(model);
    const result: AuthorModel = await this.http.put<AuthorModel>(urlPath, model).toPromise();
    console.log(result);
    return result;
  }
}
