import {
  makeStyles,
  createStyles,
  Theme
} from '@material-ui/core/styles';
import {
  Typography
} from '@material-ui/core';


const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        footer: {
            margin: theme.spacing(3),
            width: 'calc(100% - 48px)',
            color: 'rgba(0, 0, 0, 0.87)',
            textAlign: 'center',
            backgroundColor: '#fafafa',
            borderColor: 'rgba(0, 0, 0, 0.30)',
            borderStyle: 'solid',
            borderRadius: '10px',
            borderWidth: '1px'
        }
    }),
);


function Footer() {
    const classes = useStyles();

    return (
      <footer className={classes.footer}>
          <Typography
          variant="overline"
          display="block"
          gutterBottom>
            TRIGGERGRAM
          </Typography>
      </footer>
    );
}

export default Footer;
