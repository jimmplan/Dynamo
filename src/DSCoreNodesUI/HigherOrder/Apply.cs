﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Models;
using Dynamo.Nodes;
using ProtoCore.AST.AssociativeAST;

namespace DSCoreNodesUI.HigherOrder
{
    public class Apply : VariableInputNode
    {
        public Apply()
        {
            InPortData.Add(new PortData("func", "Function to apply."));
            RegisterAllPorts();
        }

        protected override string GetInputRootName()
        {
            return "arg";
        }

        protected override string GetTooltipRootName()
        {
            return "Argument #";
        }

        protected override void RemoveInput()
        {
            if (InPortData.Count > 1)
                base.RemoveInput();
        }

        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            return new[]
            {
                AstFactory.BuildAssignment(
                    GetAstIdentifierForOutputIndex(0),
                    new FunctionCallNode
                    {
                        Function = inputAstNodes[0],
                        FormalArguments = inputAstNodes.Skip(1).ToList()
                    })
            };
        }
    }
}