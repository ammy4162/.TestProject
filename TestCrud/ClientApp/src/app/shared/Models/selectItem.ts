export class SelectItem {
  public id: number;
  public displayName: string;
  public code: string;

  static getInstance(response): SelectItem {
    let instance = new SelectItem();
    instance.id = response.id;
    instance.displayName = response.displayName;
    instance.code = response.code;
    return instance;
  }
}
