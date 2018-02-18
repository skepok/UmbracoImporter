module UImporter.Interfaces {
    export interface IPromise<T> {
        then(func: (response: IBaseResponse<T>) => void): void;
    }

    export interface IBaseResponse<T> {
        data: T;
        status: number;
    }
}