import Provider from "./Provider";
import User from "../stores/models/User";
import Module from "../stores/models/Module";
import Unit from "../stores/models/Unit";
import Field from "../stores/models/Field";
import Instance from "../stores/models/Instance";

export  class Client {
  constructor(host: string) {
    this.host = host;
  }

  users = (user: string) => new Provider<User>({host: this.host, url: `${user}/users`});
  modules = (user: string) => new Provider<Module>({host: this.host, url: `${user}/modules`});
  units = (user: string, module: string) => new Provider<Unit>({host: this.host, url: `${user}/modules/${module}/units`});
  instances = (user: string, path: string) => new Provider<Instance>({host: this.host, url: `${user}/${path}`});
  fields = (user: string, module: string, unit: string) => new Provider<Field>({host: this.host, url: `${user}/modules/${module}/units/${unit}/fields`});

  host: string;
}

export const client = new Client("http://localhost:5083");

