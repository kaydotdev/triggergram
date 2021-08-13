import {
    makeStyles,
    Theme,
    createStyles
} from '@material-ui/core/styles';
import { Fab } from '@material-ui/core';
import { Add } from '@material-ui/icons';


const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    navbutton: {
      position: 'absolute',
      bottom: theme.spacing(3),
      right: theme.spacing(3)
    },
  }),
);


function NavButton() {
    const classes = useStyles();

    return (
        <Fab color="secondary" className={classes.navbutton}><Add /></Fab>
    );
}

export default NavButton;
