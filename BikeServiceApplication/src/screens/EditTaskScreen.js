import React, {Component} from 'react';
import {View, StyleSheet, Text, TextInput, TouchableOpacity} from 'react-native';
import ModalSelector from 'react-native-modal-selector';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task';
import Error from '../components/Error';

class EditTaskScreen extends Component {
  constructor(props){
    super(props);
    this.state = {
      id: Task.task.TaskId,
      header: Task.task.Header,
      description: Task.task.Description,
      state: Task.task.State,
      selectItemList: [],
      showError: false,
      error: {}   
    };
  }

  componentDidMount(){
    Task.statusList.forEach(element => {
      let item = {
        key: element.Value, 
        label: element.Label
      };
      this.setState(prevState => ({
        selectItemList: [...prevState.selectItemList, item]
      }))
    });
  }
  update(){
    Task.task.Header = this.state.header;
    Task.task.Description = this.state.description;
    Task.task.State = this.state.state;

    let res = TasksController.updateTask();

    if(res.code === 200){
      let index = TasksController.tasksList.findIndex((obj => obj.TaskId == Task.task.TaskId));
      TasksController.tasksList[index].Header = this.state.header;
      TasksController.tasksList[index].Description = this.state.description;
      this.props.navigation.navigate('ControllPanel');
    }
    else{
      this.setState({
        error: res
      });
      this.setState({ showError: true });
    }
  }

  render() {
    return (
      <View >
        <TextInput 
          value={this.state.header}
          style={styles.header}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({header: newText})}
        />
        <TextInput 
          value={this.state.description}
          multiline={true}
          style={styles.description}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({description: newText})}
          
        />
        <ModalSelector
          style={styles.select}
          data={this.state.selectItemList}
          onChange={(option)=>this.setState({state: option.key})}
          initValue={String(Task.statusList.find(x => x.Value === this.state.state).Label)}
          selectStyle={{ borderColor: "black" }}
        />
        <TouchableOpacity style={styles.searchButton} onPress={this.update.bind(this)}>
          <Text style={styles.buttonText}>Aktualizuj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }
}

export default EditTaskScreen;

const styles = StyleSheet.create({
  header:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 30
  },
  description:{
    color: '#000000',
    borderWidth: 1,
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    padding: 5,
    backgroundColor: 'white',
    height: 'auto',
    textAlignVertical: "top"
  },
  searchButton: {
    alignItems: 'center',
    marginTop: 10,
    marginLeft:'auto',
    marginRight:'auto',
    justifyContent: 'center',
    width: 100,
    padding: 5,
    backgroundColor: '#249ef0',
    borderRadius: 5,
    zIndex: 100
  },
  buttonText:{
    color: 'white'
  },
  select:{
    marginLeft: 'auto',
    marginRight: 'auto',
    marginTop: 10,
    width: 300,
    height: 40,
    backgroundColor: 'white'
  }
});
