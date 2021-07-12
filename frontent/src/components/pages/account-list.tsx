import React, { FunctionComponent } from 'react';
import { useQuery } from 'react-query';
import { useHistory } from 'react-router-dom';
import accountService from '../../serivces/account-service';
import TableContainer from '@material-ui/core/TableContainer';
import { Button, Grid, Paper } from '@material-ui/core';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import TableBody from '@material-ui/core/TableBody';
import { AxiosError } from 'axios';
import ErrorRow from '../table/error-row';
import LoadingRow from '../table/loading-row';
import EmptyRow from '../table/empty-row';
import AddIcon from '@material-ui/icons/Add';
import { Account } from '../../serivces/account-service';


const AccountRow:FunctionComponent<Account> = ({ id, balance, name }: Account) => {
  const history = useHistory();

  const detailsHandler = (): void => {
    history.push(`/accounts/${id}`);
  };
  return <TableRow key={id} className='pointer' onClick={detailsHandler}>
    <TableCell>
      {name}
    </TableCell>
    <TableCell>
      {balance}
    </TableCell>
  </TableRow>;
};

const AccountListPage = () => {
  // TODO: Types
  const query = useQuery('accounts', accountService.fetch, {
    refetchOnWindowFocus: false
  });



  return <>
    <Grid container>
      <Grid item style={{textAlign: 'right', paddingBottom: '12px'}} xs={12}>
        <Button color='primary' variant='contained' size='small' startIcon={<AddIcon/>}>Create</Button>
      </Grid>
    </Grid>
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>
              Name
            </TableCell>
            <TableCell>
              Balance
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {!(query.isError || query.isLoading || query.data?.data.length) && <EmptyRow />}
          {query.isError && <ErrorRow message={(query.error as AxiosError).message} />}
          {query.isLoading && <LoadingRow/>}
          {query.data?.data.map(account => <AccountRow {...account} />)}
        </TableBody>
      </Table>
    </TableContainer>
  </>;
};

export default AccountListPage;
