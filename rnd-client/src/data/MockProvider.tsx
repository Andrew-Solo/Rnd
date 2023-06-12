import Result from "./Result";
import Provider from "./Provider";
import {Entity} from "../models/Unit";
const { v4: uuid } = require('uuid');

export default class MockProvider<T extends Entity> extends Provider<T> {
  constructor(data: {url: string, mock: T[]}) {
    super(data);
    this.mock = data.mock;
  }

  override get(query: {[prop:string]: any} = {}): Result<T> {
    let data = this.mock;

    for (const prop in query) {
      data = data.filter(item => item[prop] === query[prop])
    }

    return new Result<T>("Объект", data[0] ?? null)
  }

  override list(query: {[prop:string]: any} = {}): Result<T[]> {
    let data = this.mock;

    for (const prop in query) {
      data = data.filter(item => item[prop] === query[prop])
    }

    return new Result<T[]>("Объекты", data ?? [])
  }

  override create(entity: T): Result<T> {
    entity.id = uuid();
    this.mock.push(entity);
    return new Result<T>("Создано", entity)
  }

  override update(entity: T): Result<T> {
    const item = this.mock.filter(item => item.id === entity.id)[0] ?? null;
    Object.assign(item, entity);
    return new Result<T>("Обновлено", item)
  }

  override delete(id: string | null = null): Result<T> {
    const item = this.mock.filter(item => item.id === id)[0] ?? null;
    const index = this.mock.indexOf(item);
    if (index > -1) this.mock.splice(index, 1);
    return new Result<T>("Удалено", item)
  }

  mock: T[]
}