import {Injectable} from '@angular/core';
import {AuthorModel} from '../shared/models/author.model';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Injectable()
export class AuthorService {
  private Api = 'api';
  private control = 'Author';
  private ActionGetAll = 'GetAll';
  private ActionCreate = 'Create';
  private ActionGet = 'Get';
  private ActionDelete = 'Delete';
  private ActionUpdate = 'Update';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll() {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionGetAll}`;
    const authors: AuthorModel[] = await this.http.get<AuthorModel[]>(urlPath).toPromise();
    return authors;
  }

  public async GetById(author: string) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionGet}/`;
    const authors: AuthorModel = await this.http.get<AuthorModel>(urlPath + author).toPromise();
    return authors;
  }

  public async AddAuthor(model: AuthorModel) {
    const urlPath = `${this.serverUrl}/${this.ActionCreate}`;
    const result: AuthorModel = await this.http.post<AuthorModel>(urlPath, model).toPromise();
    return result;
  }

  public async EditAuthor(model: AuthorModel) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionUpdate}`;
    const result: AuthorModel = await this.http.post<AuthorModel>(urlPath, model).toPromise();
    return result;
  }

  public async Delete(model: AuthorModel) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.Api}/${this.control}/${this.ActionDelete}`;
    await this.http.post(urlPath, model).toPromise();
  }
}
