import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import CircularProgress from '@material-ui/core/CircularProgress';
import React from 'react';

const LoadingRow = () => {
  return <TableRow>
    <TableCell colSpan={10} align='center'>
      <CircularProgress/>
    </TableCell>
  </TableRow>;
};

export default LoadingRow;
