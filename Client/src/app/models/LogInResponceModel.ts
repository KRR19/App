import {Responsemodel} from './Responsemodel';

export interface LogInResponceModel extends Responsemodel {
  user: string;
  accessToken: string;
  refreshToken: string;
  role: string;
}
