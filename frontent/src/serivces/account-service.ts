import axios, { AxiosResponse } from 'axios';

enum AccountType {
    general,
    cash,
    currentAccount,
    creditCard,
    accountWithOverDraft,
    savingAccount,
    bonus,
    insurance,
    investment,
    loan,
    mortgage
}

export interface Account {
    id: string;
    name: string;
    color: string;
    type: AccountType,
    balance: number
}

const API_ENDPOINT = 'http://localhost:5000';

class AccountService {
    public async fetch(): Promise<AxiosResponse<Account[]>> {
        return axios({
            method: 'GET',
            url: `${API_ENDPOINT}/api/accounts`
        });
    }

    public async createAsync(): Promise<void>{

    }

    public async deleteAsync(): Promise<void> {

    }

    public async updateAsync(): Promise<void> {
        
    }
}

const accountService = new AccountService();
export default accountService;
