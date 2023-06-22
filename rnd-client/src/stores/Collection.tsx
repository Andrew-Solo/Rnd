import {makeAutoObservable} from "mobx";
import Provider from "../data/Provider";
import Result from "../data/Result";
import Message from "../data/Message";

export default class Collection<T> {
  load(): void {
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];

    this.provider.list().then(this.loadSuccess);
  }

  loadSuccess(result: Result<T[]>): void {
    this.loaded = true;
    this.failed = !result.success;
    this.message = result.message;
    this.data = result.data.map(data => new this.type(data));
  }

  loaded: boolean
  failed: boolean
  message: Message | null
  data: T[]

  provider : Provider<T>
  type : new(data: any) => T

  constructor(provider : Provider<T>, type: new(data: any) => T) {
    this.loaded = false;
    this.failed = false;
    this.message = null;
    this.data = [];
    this.provider = provider;
    this.type = type;

    makeAutoObservable(this, {
      provider: false,
      type: false,
    }, { autoBind: true })
  }
}