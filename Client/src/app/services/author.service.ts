import {Injectable} from '@angular/core';
import {AuthorModel} from '../shared/models/author.model';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Injectable()
export class AuthorService {
  private api = 'api';
  private control = 'Author';
  private actionGetAll = 'GetAll';
  private actionCreate = 'Create';
  private actionGet = 'Get';
  private actionDelete = 'Delete';
  private actionUpdate = 'Update';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll() {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}/${this.actionGetAll}`;
    const authors: AuthorModel[] = await this.http.get<AuthorModel[]>(urlPath).toPromise();
    return authors;
  }

  public async GetById(author: string) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}/${this.actionGet}/`;
    const authors: AuthorModel = await this.http.get<AuthorModel>(urlPath + author).toPromise();
    return authors;
  }

  public async AddAuthor(model: AuthorModel) {
    const urlPath = `${this.serverUrl}/${this.actionCreate}`;
    const result: AuthorModel = await this.http.post<AuthorModel>(urlPath, model).toPromise();
    return result;
  }

  public async EditAuthor(model: AuthorModel) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}/${this.actionUpdate}`;
    const result: AuthorModel = await this.http.post<AuthorModel>(urlPath, model).toPromise();
    return result;
  }

  public async Delete(model: AuthorModel) {
    const urlPath = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}/${this.actionDelete}`;
    await this.http.post(urlPath, model).toPromise();
  }
}
