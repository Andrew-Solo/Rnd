
export default abstract class Model {
  readonly id: string
  path: string
  name: string
  title: string | null
  subtitle: string | null
  description: string | null
  icon: string | null
  color: number[] | null
  subcolor: number[] | null
  thumbnail: string | null
  image: string | null
  subimage: string | null
  attributes: {[name:string]: any}
  order: number
  readonly created: Date
  viewed: Date
  updated: Date | null

  protected constructor(data: {id: string, name: string, path: string, [key:string]: any}) {
    this.id = data.id;
    this.path = data.path;
    this.name = data.name;
    this.title = data.title ?? null;
    this.subtitle = data.subtitle ?? null;
    this.description = data.description ?? null;
    this.icon = data.icon ?? null;
    this.color = data.color ?? null;
    this.subcolor = data.subcolor ?? null;
    this.thumbnail = data.thumbnail ?? null;
    this.image = data.image ?? null;
    this.subimage = data.subimage ?? null;
    this.attributes = data.attributes ?? {};
    this.order = data.order ?? 0;
    this.created = data.created ?? new Date();
    this.viewed = data.viewed ?? new Date();
    this.updated = data.updated ?? null;
  }
}