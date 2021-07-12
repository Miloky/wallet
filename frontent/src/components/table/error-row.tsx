import React, { FunctionComponent } from 'react';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';
import { Typography } from '@material-ui/core';

interface ErrorRowProps {
  colSpan?: number;
  message: string;
}

const ErrorRow: FunctionComponent<ErrorRowProps> = ({colSpan = 10, message}) => {
  return <TableRow>
    <TableCell colSpan={colSpan} align='center'>
      <Typography color='secondary'>
        {message}
      </Typography>
    </TableCell>
  </TableRow>;
};

export default ErrorRow;
