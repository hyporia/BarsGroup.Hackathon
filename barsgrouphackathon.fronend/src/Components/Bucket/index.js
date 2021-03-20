
import { Button, IconButton } from '@material-ui/core';
import Box from '@material-ui/core/Box';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import DeleteIcon from '@material-ui/icons/Delete';
import { useSnackbar } from 'notistack';
import React, { useEffect, useState } from 'react';
import { DeleteFilesFromBucket, GetFiles } from '../../Client';

function Bucket({ login, error }) {
    const { enqueueSnackbar } = useSnackbar();
    useEffect(x => {
        GetFiles(null, true)
            .then(files => {
                setFiles(files);
            })
            .catch(e => {

            });
    }, []);

    const [files, setFiles] = useState([]);
    const dummyImg = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAIAAABMXPacAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAABmJLR0QA7wDvAO/BzIMFAAAAB3RJTUUH5QMUEDAZtY2nygAAA85JREFUeNrt3Vtf2koUxuE1xyRAQsTv/xl3RY45zPSClr1F6sa25F3q+1x6Icn6m5mRH7bm0PVCOBZ9AV8dA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxABgDgDEAGAOAMQAYA4AxAJhHX8C/zISvldE3e6YiwDCOfT+klCZ7RWttCN47h751BQG2u8N6sxmGceo7925ZL2ZVib198B5wOHbf1s/TT19EhmH85+n5cOywEwAH2O7244Qrz4Uxpe1uj50AMkDKeRjGKffeC0ZkGMaUkVsydA/IcvU8Erwvi+i8yykfu+7Y9fmOM8qSpz2BvYTfhC/MZ1VTz8/nk5xnu/3hab0BrlR3pesXsaos2qb+7+nQGDOfVU2zMAa4Vt2RogDGmMW8svbKoGdlGYO6h/WvUBTAORt8uH6V1oQQ3vn9PgZFAYyYNzbDdy1BMYSqLD7EqqXouR5TGsfk7JWfiZxlGIYbv09ZxIdl45x93uzWm+09T1B/gaInIKW03x9ef92IdH1/vO2fWC6L+NA23jtjTL2YN4u58udAUQAR2ez221cN+mF4Wj/f8lbdj+n/PEQZI/obKFqCRCSl9O1p3XV9VRbO2Zzzseu3231/w/pzMf2TUwMRUbsW6QogIinlzXa33e2tMVlySjdN7er0T5Q3UBfgJOc83jysN6Z/YozUtdIGuvaA12IMTT33/pfD/d/pnxiRuta4H6gOUMTw2DZtUz+2y+CvPKw3Tv/EiNSLWVUW6Nt6QW+AIoZV23jvc84xhlXbXDR41/RPjDHW6rplXVdzdp7++SsXDX5j+jppDFDEuGqX/tWac27waaYvCk9BRYyrtvnVrhtjeFwtrTHuU0xftAWIIbwx/ZOru/HHpWsJijG8Pf3PR1eAL4gBwHStpymlW953+8OXQN/lC7oC7A/He39UTdt7QdAARi4+kZNznnxABvihIMHuAdYY7x3wBzKLeO8s9O058CY8n1UO9+aMs3Y+q7ATAAcoi/iwrCFnf+/dw7Iui4idgNHw/wl/5T/QUBHgx6VM+Fp6TkKKjqF6hjIl/iYMxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDADGAGAMAMYAYAwAxgBgDAD2HbZY+3G+16ZIAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDIxLTAzLTIwVDE2OjQ4OjI1LTA0OjAwuxxFEQAAACV0RVh0ZGF0ZTptb2RpZnkAMjAyMS0wMy0yMFQxNjo0ODoyNS0wNDowMMpB/a0AAAAASUVORK5CYII=";


    const handleDeleteFile = (file) => {
        DeleteFilesFromBucket([file.id])
            .then(resp => {
                const index = files.indexOf(file);
                if (index > -1) {
                    var fs = files.map(x => x);
                    fs.splice(index, 1);
                    setFiles(fs);
                }
                enqueueSnackbar("Файл удален", { variant: 'success' });
            })
            .catch(e => {
                enqueueSnackbar("Ошибка при удалении файла", { variant: 'error' });
            });
    }

    const deleteAllFilesHandler = () => {
        DeleteFilesFromBucket(null)
            .then(resp => {
                enqueueSnackbar("Файлы удалены", { variant: 'success' });
                setFiles([]);
            })
            .catch(e => {
                enqueueSnackbar("Ошибка при удалении файлов", { variant: 'error' });
            });
    }

    return (
        <Box display="flex" flex="1 1 0" flexDirection='column'>
            <Box margin={2}>
                <Button alignSelf="start" onClick={() => deleteAllFilesHandler()}>Очистить корзину</Button>
            </Box>
            <Box display="flex" flexDirection="row" flex={1}>

                {files.map(file => (<Card style={{ maxWidth: '200px', maxHeight: '250px', margin: '5px' }}>
                    <CardActionArea>
                        <CardMedia
                            component="img"
                            alt="File"
                            height="140"
                            src={dummyImg}
                            title={file.name}
                        />
                        <CardContent>
                            <Typography variant="body2" color="textSecondary" component="p">
                                {file.name?.length > 17 ? file.name.substring(0, 17).concat('...') : file.name}
                            </Typography>
                        </CardContent>
                    </CardActionArea>
                    <Box display="flex" flexDirection='row' alignSelf='bottom' >
                        <CardActions>
                            <IconButton onClick={() => handleDeleteFile(file)}>
                                <DeleteIcon />
                            </IconButton>
                        </CardActions>
                    </Box>

                </Card>))}
            </Box>
        </Box>
    )
}

export default Bucket;