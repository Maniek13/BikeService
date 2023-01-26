import React, {Component} from 'react';
import {View, StyleSheet, Text, TextInput, TouchableOpacity} from 'react-native';
import ModalSelector from 'react-native-modal-selector';

import TasksController from '../controllers/TasksController';
import Task from '../objects/Task';
import Error from '../components/Error';
import Settings from '../objects/Settings';

import MainStyles from '../styles/MainStyles';
import EditTaskScreenStyles from '../styles/EditTaskScreenStyles';

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
          style={mainStyle.taskHeader}
          placeholder="tytuł" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({header: newText})}
        />
        <TextInput 
          value={this.state.description}
          multiline={true}
          style={mainStyle.taskDescription}
          placeholder="opis" 
          placeholderTextColor="gray" 
          onChangeText={newText => this.setState({description: newText})}
          
        />
        <ModalSelector
          style={editTaskScreenStyle.select}
          data={this.state.selectItemList}
          onChange={(option)=>this.setState({state: option.key})}
          initValue={String(Task.statusList.find(x => x.Value === this.state.state).Label)}
          selectStyle={{ borderColor: "black" }}
        />
        <TouchableOpacity style={mainStyle.button} onPress={this.update.bind(this)}>
          <Text style={mainStyle.buttonText}>Aktualizuj</Text>
        </TouchableOpacity>
        {this.state.showError === true ? <Error error = {this.state.error.data}/> : ''}
      </View>
    );
  }
}

export default EditTaskScreen;

const mainStyle = MainStyles[Settings.SchemaStyle]
const editTaskScreenStyle = EditTaskScreenStyles[Settings.SchemaStyle]

