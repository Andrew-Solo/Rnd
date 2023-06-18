import {makeAutoObservable} from "mobx";
import Store from "./Store";
import Module from "../models/Module";
import {client} from "../data/Client";
import Message from "../data/Message";
import Result from "../data/Result";


export default class Modules {
   constructor(store: Store) {
    this.store = store;
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];

    makeAutoObservable(this, {
      store: false
    }, { autoBind: true })
  }

  syncModules(): void {
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];

    const userId = this.store.session.user?.id;
    client.modules().list(userId ?? "@guest").then(this.syncModulesThen);
  }

  syncModulesThen(result: Result<Module[]>): void {
    this.loaded = true;
    this.failed = !result.success;
    this.message = result.message;
    this.data = result.data;
  }

  readonly store: Store
  loaded: boolean
  failed: boolean
  message: Message | null
  data: Module[]
}