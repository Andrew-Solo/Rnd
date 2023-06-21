import Session from "./Session";
import Module from "./models/Module";
import {client} from "../data/Client";
import Collection from "./Collection";
import Unit from "./models/Unit";

export class Store {
  constructor() {
    this.session = new Session();
    this.modules = new Collection<Module>(client.modules(this.user))
    this.modules.load();
  }

  get user(): string {
    return this.session.user?.name ?? "@guest";
  }

  session: Session
  modules: Collection<Module>

  createUnits(module: string): Collection<Unit> {
    const collection = new Collection<Unit>(client.units(this.user, module));
    collection.load();
    return collection;
  }
}

export const store = new Store();