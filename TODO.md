
# Todo list for BindOpen

## Next tasks

### 🚀 Performance & AOT Readiness

- [ ] **Switch to Manual Extension Registration**: Replace expensive assembly scanning (`AddAssemblyFrom`) with explicit registration to eliminate startup latency and ensure Native AOT compatibility.
    * Example:
        ```csharp
        scope.ExtensionStore.Add(BdoData.NewTaskDefinition<TaskFake>("tests.task"));
        ```
- [ ] **Implement Creation Delegates**: Avoid `Activator.CreateInstance` by providing direct instantiation logic.
    * Example:
        ```csharp
        .WithCreationDelegate(() => new TaskFake())
        ```
- [ ] **Develop Source Generators**: Automate the mapping logic between `BdoMetaObject` and POCOs to remove runtime Reflection (`GetValue`/`SetValue`).

### 🛡️ Scripting & Robustness

- [ ] **Static Script Validation**: Create a utility to pre-compile and validate script syntax in configuration files before runtime.
    * Example:* Running `interpreter.Evaluate(script)` against a mock context during unit tests.
- [ ] **Standardize Serialization Syntax**: Ensure consistency between `$this` (keyword) and function calls to avoid "Magic String" mismatches in `ToString()` output.
- [ ] **Strongly-Typed Script Functions**: Map function names to constants instead of hardcoded strings.
    * *Example:* Use `public const string Name = "testEqual";` in your function classes.

### 🏗️ Architecture & DX (Developer Experience)

- [ ] **Fluent Encapsulation Wrappers**: Hide BindOpen complexity behind simple service interfaces.
    * Example:
        ```csharp
        public void RunFakeTask(bool val) => _scope.Execute(new TaskFake { BoolValue = val });
        ```
- [ ] **Manual Mapping Interface**: Implement a "Fast-Path" mapping interface to bypass the automatic Reflection-based mapper.
    * *Example:* Adding a `Map(IBdoMetaObject meta)` method to your Entities for direct assignment.
- [ ] **Schema-Driven Documentation**: Use `BdoSchema` to enforce data contracts and generate human-readable error logs automatically.

### 🧪 Advanced Testing

- [ ] **Recursive Tree Validation**: Add regression tests for deep hierarchical structures (5+ levels) in `TreeFake`.
- [ ] **Runtime Mode Simulation**: Validate that tasks behave correctly in `RuntimeModes.Simulation`, ensuring logs are populated without data side effects.

### Metadata Management

  - [ ] Meta schema should be null by default.
  
  Correct Validation so that the schema can be null and the meta set items can be inspected despite.


- [ ] Tuple dictionary approach.
```csharp
var config = NewConfig("test-config",
    ("comment", "Sunny day"),
    ("temperature", new[] { 25, 26, 26 }), // Tableau = Multi-scalar
    ("date", DateTime.Now),
    NewNode("subscriber",
        ("name", "Ernest E."),
        ("code", 1560)
    )
)
```
- [ ] Anonymous Object
```csharp
var config = NewConfig("test-config", new {
    comment = "Sunny day",
    temperature = new[] { 25, 26, 26 },
    date = DateTime.Now,
    subscriber = NewNode(new {
        name = "Ernest E.",
        code = 1560
    })
})
```
- [ ] "String-Driven" Approach
```csharp
var config = NewConfig("test-config")
    .With("comment:Sunny day", "temperature:25,26,26")
    .With("date:" + DateTime.Now)
    .WithNode("subscriber", "name:Ernest E.", "code:1560")
```
## Stuff to check out later on

### v.1.3
* Loading connectors, entities, functions, tasks

### v.1.2
* Integrate .net core logging/config

### v.1.1

* Instantiate host with AddBindOpen method
* See whether startup logger makes sense
* SetLogger must be guessed automatically
* ScriptingFactory -> BdoScript
* Simplify script interpretation
* Improve extension handling
* Make logging work again
* Add Getting Help + Contributors sections in README.md
* Handle expcetions correctly.
* Simplify extension item management
* Improve OptionsSet 
* Implement a relevant readme file
* Add Expression data value type
* Optimize serialization
* Improve AppModule management (DbFluent mode)
* Make XSD files work
* ToApiDto: Use standard Result class
* SetConfiguration Extension


