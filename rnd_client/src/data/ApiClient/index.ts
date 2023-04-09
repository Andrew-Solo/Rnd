import UsersController from "@/data/ApiClient/Controllers/UsersController";

class ApiClient {
  constructor(host: string) {
    this.Users = new UsersController(host);
  }
  public Users: UsersController;
}

export default ApiClient;