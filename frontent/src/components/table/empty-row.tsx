import React, { FunctionComponent } from 'react';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';

interface EmptyRowProps {
  colSpan?: number
}

const EmptyRow: FunctionComponent<EmptyRowProps> = ({colSpan = 10}) => {
  return <TableRow>
    <TableCell colSpan={colSpan} align='center'>
      No data
    </TableCell>
  </TableRow>;
};

export default EmptyRow;
