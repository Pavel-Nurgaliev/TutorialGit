var engine = new WorkflowEngine();

var workflow = new Workflow();
workflow.AddActivity(new FetchEmployeeDataActivity());
workflow.AddActivity(new AwaitManagerApprovalActivity());
workflow.AddActivity(new SendConfirmationEmailActivity());

engine.Run(workflow);