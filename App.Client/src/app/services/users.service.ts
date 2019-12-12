import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {UserInfoModel} from '../shared/models/user-info.model';
import {RolesModel} from '../shared/models/roles.model';
import {ChangeRoleModel} from '../shared/models/change-role.model';

@Injectable()
export class UsersService {
  private api = 'api';
  private control = 'User';
  private actionChangeRole = 'ChangeRole';
  private actionGetAll = 'GetAll';
  private actionGetAllRoles = 'GetAllRoles';
  private serverUrl = `${environment.protocol}://${environment.host}:${environment.port}/${this.api}/${this.control}`;

  constructor(private  http: HttpClient) {
  }

  public async GetAll(): Promise<UserInfoModel[]> {
    const urlPath = `${this.serverUrl}/${this.actionGetAll}`;
    const result: UserInfoModel[] = await this.http.get<UserInfoModel[]>(urlPath).toPromise();
    return result;
  }

  public async GetAllRoles(): Promise<RolesModel[]> {
    const urlPath = `${this.serverUrl}/${this.actionGetAllRoles}`;
    const result: RolesModel[] = await this.http.get<RolesModel[]>(urlPath).toPromise();
    return result;
  }

  public async ChangeRole(RoleModel: ChangeRoleModel) {
    const urlPath = `${this.serverUrl}/${this.actionChangeRole}`;
    const result: ChangeRoleModel = await this.http.post<ChangeRoleModel>(urlPath, RoleModel).toPromise();
    return result;
  }
}
